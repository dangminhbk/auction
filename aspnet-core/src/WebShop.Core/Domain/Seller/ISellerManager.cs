using Abp.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Domain.Seller
{
    public interface ISellerManager : IDomainService
    {
        Task RegisterSeller(
            long UserId,
            string Name,
            string Address,
            string PhoneNumber,
            string EmailAddress,
            string Description
        );
        Task UpdateSellerInfo(
            long SellerId,
            string Name,
            string Address,
            string PhoneNumber,
            string Description,
            long? CoverImage,
            long? Logo
        );

        Task<Seller> GetSellerByUserId(long UserId);
        Task<Seller> GetSellerById(long SellerId);
        Task<SellerPaymentOption> GetSellerPaymentOption(long SellerId);
        Task UpdatePayment(
            long SellerId,
            List<KeyValuePair<string, string>> Payload,
            PaymentOption paymentOption
        );
        Task DeleteSeller(long Id);
        Task<IQueryable<Seller>> GetAllSeller();
        Task<IQueryable<Seller>> SearchSeller(string keyword);
    }
}
