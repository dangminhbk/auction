using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Auction
{
    public interface IAuctionManager : IDomainService
    {
        Task SetCurrentPrice(
            long AuctionId,
            decimal Price,
            long UserId,
            DateTime Time
        );

        Task CreateAuction(
            long ProductId,
            decimal MinPrice,
            decimal MinAcceptPrice,
            DateTime StartTime,
            DateTime EndTime,
            long SellerId
        );

        Task DeleteAuction(
            long id
        );

        Task<Auction> GetDetail(
            long id
        );

        Task<IQueryable<Auction>> GetAll(
        );

        Task<IQueryable<Bid.Bid>> GetBids(long auctionId);

        Task<Auction> MakeBid(
        long AuctionId,
        decimal Price,
        long UserId,
        DateTime Time);
    }
}
