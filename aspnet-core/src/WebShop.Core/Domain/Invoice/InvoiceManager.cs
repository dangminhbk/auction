using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Domain.Invoice
{
    public class InvoiceManager : DomainService, IInvoiceManager
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<Auction.Auction, long> _auctionRepository;
        private readonly IRepository<Bid.Bid, long> _bidRepository;
        public InvoiceManager(
            IRepository<Invoice, long> invoiceRepository,
            IRepository<Auction.Auction, long> auctionRepository,
            IRepository<Bid.Bid, long> bidRepository
            )
        {
            _auctionRepository = auctionRepository;
            _invoiceRepository = invoiceRepository;
            _bidRepository = bidRepository;
        }
        public async Task ChangeStatus(long Id, OrderStatus status)
        {
            Invoice Invoice = await Get(Id);
            if (Invoice.PaymentStatus == OrderStatus.Pending)
            {
                Invoice.PaymentStatus = status;
                return;
            }
            throw new UserFriendlyException("Chỉ có thể cập nhập hóa đơn ở trạng thái chờ");
        }

        [UnitOfWork]
        public async Task Create(string ProductName, decimal SubTotal, long AuctionId, string serial)
        {
            Auction.Auction auction = await _auctionRepository
                .FirstOrDefaultAsync(s => s.Id == AuctionId);

            if (auction.HasMakeInvoice)
            {
                throw new UserFriendlyException("Hóa đơn đã được lập trước đó!");
            }

            Bid.Bid lastBid = _bidRepository.GetAll()
                .Where(s => s.AuctionId == AuctionId)
                .OrderByDescending(s => s.BidTime)
                .FirstOrDefault();

            if (auction.EndDate > DateTime.UtcNow)
            {
                throw new UserFriendlyException("Phiên đấu giá chưa kết thúc");
            }

            auction.HasMakeInvoice = true;

            Invoice Invoice = new Invoice
            {
                AuctionId = AuctionId,
                SubTotal = SubTotal,
                BidId = lastBid.Id,
                ProductName = ProductName,
                SellerId = auction.SellerId,
                UserId = lastBid.UserId,
                SerialNumber = serial
            };

            await _auctionRepository.UpdateAsync(auction);
            await _invoiceRepository.InsertAsync(Invoice);
        }

        public async Task<Invoice> Get(long Id)
        {
            return (await GetAll()).FirstOrDefault(s => s.Id == Id);
        }

        public async Task<IQueryable<Invoice>> GetAll()
        {
            return _invoiceRepository
                .GetAllIncluding(
                s => s.Seller,
                s => s.User,
                s => s.Auction,
                s => s.Bid
                );
        }

        public async Task UpdateAddress(long Id, string Address, string PhoneNumber, string ReceiperName)
        {
            Invoice Invoice = await Get(Id);

            if (Invoice.PaymentStatus == OrderStatus.Initial)
            {
                Invoice.Address = Address;
                Invoice.PhoneNumber = PhoneNumber;
                Invoice.ReceiperName = ReceiperName;
                Invoice.PaymentStatus = OrderStatus.Pending;
                await _invoiceRepository.UpdateAsync(Invoice);
                return;
            }

            throw new UserFriendlyException("Không thể cập nhập");
        }
    }
}
