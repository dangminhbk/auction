using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Seller
{
    public class SellerManager : DomainService, ISellerManager
    {
        private readonly IRepository<Seller, long> SellerRepository;
        private readonly IRepository<SellerPaymentOption, long> SellerPaymentRepository;
        public SellerManager(
            IRepository<Seller, long> sellerRepository,
            IRepository<SellerPaymentOption, long> sellerPaymentRepository
        )
        {
            SellerPaymentRepository = sellerPaymentRepository;
            SellerRepository = sellerRepository;
        }
        public async Task DeleteSeller(long Id)
        {
            await SellerRepository.DeleteAsync(Id);
        }

        public async Task<IQueryable<Seller>> GetAllSeller()
        {
            return (await GetAllInternal()).AsNoTracking();
        }

        private async Task<IQueryable<Seller>> GetAllInternal()
        {
            return this.SellerRepository.GetAllIncluding(
                s => s.SellerLogo,
                s => s.SellerLogo.Image,
                s => s.SellerCover,
                s => s.SellerCover.Image,
                s => s.User
            );
        }

        private async Task<Seller> GetInternal(long id)
        {
            return (await GetAllInternal()).FirstOrDefault(s => s.Id == id);
        }

        public async Task<Seller> GetSellerById(long SellerId)
        {
            return (await GetAllSeller()).FirstOrDefault(s => s.Id == SellerId);
        }

        public async Task<Seller> GetSellerByUserId(long UserId)
        {
            return (await GetAllSeller()).FirstOrDefault(s => s.UserId == UserId);
        }

        public async Task<SellerPaymentOption> GetSellerPaymentOption(long SellerId)
        {
            var seller = SellerRepository
                .GetAllIncluding(s => s.SellerPaymentOption)
                .FirstOrDefault(s => s.Id == SellerId);
            return seller.SellerPaymentOption;
        }

        public async Task RegisterSeller(
            long UserId,
            string Name,
            string Address,
            string PhoneNumber,
            string EmailAddress,
            string Description)
        {
            var seller = new Seller
            {
                Address = Address,
                Description = Description,
                EmailAddress = EmailAddress,
                Name = Name,
                UserId = UserId,
                PhoneNumber = PhoneNumber,
                PaymentRegisterStatus = PaymentRegisterStatus.UnRegistered
            };

            var payment = new SellerPaymentOption
            {
                PaymentOption = PaymentOption.Unset
            };

            seller.SellerPaymentOption = payment;

            await SellerRepository.InsertAsync(seller);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<IQueryable<Seller>> SearchSeller(string keyword)
        {
            return (await this.GetAllSeller())
                .WhereIf(!keyword.IsNullOrEmpty(),
                    s => s.Name.Contains(keyword)
            );
        }

        public async Task UpdatePayment(long SellerId,
            List<KeyValuePair<string, string>> Payload,
            PaymentOption paymentOption
            )
        {
            var payment = await GetSellerPaymentOption(SellerId);
            payment.PaymentOption = paymentOption;

            payment.ExtensionData = "";
            foreach (var item in Payload)
            {
                payment.SetData(item.Key, item.Value);
            }

            await SellerPaymentRepository.UpdateAsync(payment);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSellerInfo(
            long SellerId,
            string Name,
            string Address,
            string PhoneNumber,
            string Description,
            long? CoverImage,
            long? Logo)
        {
            var seller = await SellerRepository.GetAsync(SellerId);

            if (CoverImage.HasValue)
            {
                var oldImage = seller.SellerCover?.Image;
                if (oldImage == null || oldImage?.Id != CoverImage.Value)
                {
                    var cover = new SellerCover
                    {
                        ImageId = CoverImage.Value
                    };

                    seller.SellerCover = cover;
                }
            }

            if (Logo.HasValue)
            {
                var oldImage = seller.SellerCover?.Image;
                if (oldImage == null || oldImage.Id != Logo.Value)
                {
                    var logo = new SellerLogo
                    {
                        ImageId = Logo.Value
                    };
                    seller.SellerLogo = logo;
                }
            }

            seller.Name = Name;
            seller.Address = Address;
            seller.PhoneNumber = PhoneNumber;
            seller.Description = Description;

            await SellerRepository.UpdateAsync(seller);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
