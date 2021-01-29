using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Domain.Auction
{
    public class AuctionManager : DomainService, IAuctionManager
    {
        private readonly IRepository<Auction, long> _auctionRepository;
        private readonly IRepository<Product.Product, long> _productRepository;
        private readonly IRepository<Bid.Bid, long> _bidRepository;
        private readonly IRepository<Seller.Seller, long> _sellerRepository;
        public AuctionManager(
            IRepository<Auction, long> auctionRepository,
            IRepository<Product.Product, long> productRepository,
            IRepository<Bid.Bid, long> bidRepository,
            IRepository<Seller.Seller, long> sellerRepository
        )
        {
            _productRepository = productRepository;
            _bidRepository = bidRepository;
            _auctionRepository = auctionRepository;
            _sellerRepository = sellerRepository;
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
            var seller = await _sellerRepository.GetAsync(SellerId);

            if (seller.Credit < 1)
            {
               throw new UserFriendlyException("Bạn cần nạp tiền để tạo phiên đấu giá");
            }

            if (StartTime > EndTime || EndTime < DateTime.UtcNow)
            {
                throw new UserFriendlyException("Hãy nhập thời gian hợp lệ");
            }
            Auction Auction = new Auction
            {
                MinAcceptPrice = MinAcceptPrice,
                EndDate = EndTime,
                InitPrice = InitPrice,
                StartDate = StartTime,
                SellerId = SellerId,
                ProductId = ProductId
            };

            seller.Credit -= 1;

            await _sellerRepository.UpdateAsync(seller);
            await _auctionRepository.InsertAsync(Auction);
        }

        public async Task DeleteAuction(long id)
        {
            Auction auction = await _auctionRepository.GetAsync(id);
            if (auction.StartDate > DateTime.UtcNow)
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

        public async Task<IQueryable<Bid.Bid>> GetBids(long auctionId)
        {
            return _bidRepository.GetAll().Where(s => s.AuctionId == auctionId);
        }

        public async Task<Auction> GetDetail(long id)
        {
            Auction auction = await _auctionRepository.GetAll()
                .Include(s => s.Winner)
                .FirstAsync(s => s.Id == id);
            return auction;
        }

        [UnitOfWork]
        public async Task<Auction> MakeBid(long AuctionId, decimal Price, long UserId, DateTime Time)
        {
            Auction auction = await _auctionRepository.GetAsync(AuctionId);

            if (auction.WinnerId == UserId)
            {
                throw new Exception("Bạn đang là người trả giá cao nhất !");
            }

            if (Price % 500 != 0)
            {
                throw new Exception("Giá chỉ có thể lẻ đến 500VND");
            }

            if (auction.EndDate <= DateTime.UtcNow)
            {
                throw new Exception("Thời gian đấu giá đã kết thúc");
            }
            if (auction.InitPrice + 1000 >= Price)
            {
                throw new Exception("Mức giá không hợp lệ, cần lớn hơn giá khởi  tối thiểu 1000");
            }

            if (auction.CurrentPrice + 1000 >= Price)
            {
                throw new Exception("Mức giá không hợp lệ, cần lớn hơn giá hiện tại tối thiểu 1000");
            }

            if (auction.LastBidTime > Time)
            {
                throw new Exception("Mức giá không được ghi nhận");
            }

            auction.CurrentPrice = Price;
            auction.WinnerId = UserId;
            auction.LastBidTime = Time;
            auction.NumberOfBid += 1;
            Bid.Bid bid = new Bid.Bid
            {
                AuctionId = auction.Id,
                UserId = UserId,
                BidPrice = Price,
                BidTime = Time
            };

            await _auctionRepository.UpdateAsync(auction);
            await _bidRepository.InsertAsync(bid);
            await CurrentUnitOfWork.SaveChangesAsync();

            return auction;
        }

        public async Task SetCurrentPrice(long AuctionId, decimal Price, long UserId, DateTime Time)
        {
            Auction auction = await _auctionRepository.GetAsync(AuctionId);
            auction.CurrentPrice = Price;
            auction.WinnerId = UserId;
            auction.LastBidTime = Time;
            await _auctionRepository.UpdateAsync(auction);
        }
    }
}
