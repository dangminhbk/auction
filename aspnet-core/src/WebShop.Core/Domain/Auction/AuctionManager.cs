using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Auction
{
    public class AuctionManager : DomainService, IAuctionManager
    {
        private readonly IRepository<Auction, long> _auctionRepository;
        private readonly IRepository<Product.Product, long> _productRepository;
        private readonly IRepository<Bid.Bid, long> _bidRepository;
        public AuctionManager(
            IRepository<Auction, long> auctionRepository,
            IRepository<Product.Product, long> productRepository,
            IRepository<Bid.Bid, long> bidRepository
        )
        {
            _productRepository = productRepository;
            _bidRepository = bidRepository;
            _auctionRepository = auctionRepository;
        }
        public async Task CreateAuction(
            long ProductId,
            decimal InitPrice,
            decimal MinAcceptPrice,
            DateTime StartTime,
            DateTime EndTime,
            long SellerId
        )
        {
            var Auction = new Auction
            {
                MinAcceptPrice = MinAcceptPrice,
                EndDate = EndTime,
                InitPrice = InitPrice,
                StartDate = StartTime,
                SellerId = SellerId,
                ProductId = ProductId
            };

            await _auctionRepository.InsertAsync(Auction);
        }

        public async Task DeleteAuction(long id)
        {
            var auction = await _auctionRepository.GetAsync(id);
            if (auction.StartDate > DateTime.Now)
            {
                await _auctionRepository.DeleteAsync(id);
            }
            else
            {
                throw new Exception("Không thể xóa phiên đấu giá đang hoạt động");
            }
        }

        public async Task<IQueryable<Auction>> GetAll()
        {
            return _auctionRepository.
                GetAllIncluding(
                    s => s.Product,
                    s => s.Product.CoverImage,
                    s => s.Product.CoverImage.Image
            );
        }

        public Task<IQueryable<Bid.Bid>> GetBids(long auctionId)
        {
            throw new NotImplementedException();
        }

        public async Task<Auction> GetDetail(long id)
        {
            var auction = await _auctionRepository.GetAsync(id);
            return auction;
        }

        [UnitOfWork]
        public async Task<Auction> MakeBid(long AuctionId, decimal Price, long UserId, DateTime Time)
        {
            var auction = await _auctionRepository.GetAsync(AuctionId);
            if (auction.EndDate > DateTime.Now && auction.CurrentPrice < Price)
            {
                auction.CurrentPrice = Price;
                var bid = new Bid.Bid
                {
                    AuctionId = auction.Id,
                    UserId = UserId,
                    BidPrice = Price,
                    BidTime = Time
                };

                await _auctionRepository.UpdateAsync(auction);
                await _bidRepository.InsertAsync(bid);
            } else
            {
                throw new Exception("Bid fail");
            }

            return auction;
        }

        public async Task SetCurrentPrice(long AuctionId, decimal Price, long UserId, DateTime Time)
        {
            var auction = await _auctionRepository.GetAsync(AuctionId);
            auction.CurrentPrice = Price;
            auction.WinnerId = UserId;
            auction.LastBidTime = Time;
            await _auctionRepository.UpdateAsync(auction);
        }
    }
}
