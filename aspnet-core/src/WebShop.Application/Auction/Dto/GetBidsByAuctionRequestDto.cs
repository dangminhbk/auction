using Abp.Application.Services.Dto;

namespace WebShop.Auction.Dto
{
    public class GetBidsByAuctionRequestDto : PagedResultRequestDto
    {
        public long AuctionId { get; set; }
    }
}
