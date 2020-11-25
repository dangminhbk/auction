using Abp.Application.Services.Dto;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Authorization;
using Abp.Dependency;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebShop.Auction.Constants;
using WebShop.Auction.Dto;
using WebShop.Authorization;
using WebShop.Authorization.Users;
using WebShop.Domain.Auction;

namespace WebShop.Auction
{
    public class AuctionHub : AbpHubBase, ITransientDependency
    {
        private readonly IAuctionManager _auctionManager;
        private readonly UserManager _userManager;
        public AuctionHub(IAuctionManager auctionManager, UserManager userManager)
        {
            _auctionManager = auctionManager;
            _userManager = userManager;
        }
        public async Task JoinAuction(EntityDto<long> input)
        {
            string auctionName = $"auction-{input.Id}";
            await Groups.AddToGroupAsync(Context.ConnectionId, auctionName);
        }

        [AbpAuthorize(PermissionNames.Buyers)]
        public async Task CallPrice(PlaceBidDto input)
        {
            long UserId = AbpSession.UserId.Value;
            User User = await _userManager.FindByIdAsync(UserId.ToString());
            DateTime timeStamp = DateTime.UtcNow;
            string auctionName = $"auction-{input.AuctionId}";
            try
            {
                Domain.Auction.Auction newValue = await _auctionManager.MakeBid(input.AuctionId, input.Price, UserId, timeStamp);
                await Clients.Group(auctionName).SendAsync(AuctionHubEventNames.NewBidIsValid, new NewBidDto
                {
                    BidTime = timeStamp,
                    Price = input.Price,
                    Username = User.UserName,
                    AuctionId = input.AuctionId
                });
                await Clients.User(UserId.ToString()).SendAsync(AuctionHubEventNames.BidSuccess);
            }
            catch (Exception)
            {
                await Clients.User(UserId.ToString()).SendAsync(AuctionHubEventNames.BidFail);
            }

        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
