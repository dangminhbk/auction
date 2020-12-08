using Abp.Domain.Services;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Domain.Invoice
{
    public interface IInvoiceManager : IDomainService
    {
        Task Create(
            string ProductName,
            decimal SubTotal,
            long AuctionId,
            string SerialNumber
        );
        Task UpdateAddress(
            long Id,
            string Address,
            string PhoneNumber,
            string ReceiperName);
        Task<Invoice> Get(long Id);
        Task<IQueryable<Invoice>> GetAll();
        Task ChangeStatus(
            long Id,
            OrderStatus status);
    }
}
