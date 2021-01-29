using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Domain.Statistic;
using WebShop.Extension;
using WebShop.Seller.Dto;

namespace WebShop.Seller
{
    public class SellerAppService : WebShopAppServiceBase
    {
        private readonly IStatisticDomainService _statisticDomainService;

        public SellerAppService(IStatisticDomainService statisticDomainService)
        {
            _statisticDomainService = statisticDomainService;
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task UpdateSellerInfo(UpdateSellerDto input)
        {
            long sellerId = (await GetCurrentSeller()).Id;
            await SellerManager.UpdateSellerInfo(
                sellerId,
                input.SellerName,
                input.Address,
                input.PhoneNumber,
                input.Description,
                input.SellerCover,
                input.SellerLogo
            );
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task UpdatePayment(UpdatePaymentDto input)
        {
            long sellerId = (await GetCurrentSeller()).Id;
            await SellerManager.UpdatePayment(
                sellerId,
                input.Payload,
                input.SellerPaymentOption
            );
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<SellerDetailDto> GetYourSellerInfo()
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            return new SellerDetailDto
            {
                Id = seller.Id,
                Name = seller.Name,
                Address = seller.Address,
                Description = seller.Description,
                EmailAddress = seller.EmailAddress,
                PaymentRegisterStatus = seller.PaymentRegisterStatus,
                PhoneNumber = seller.PhoneNumber,
                SellerCoverId = seller.SellerCover?.Image?.Id,
                SellerCoverUrl = seller.SellerCover?.Image?.Url,
                SellerLogoId = seller.SellerLogo?.Image?.Id,
                SellerLogoUrl = seller.SellerLogo?.Image?.Url,
                UserId = seller.UserId,
                UserName = seller.User.UserName,
                Credit = seller.Credit
            };
        }

        public async Task<PublicDetailSellerDto> GetPublicSellerInfo(long SellerId)
        {
            Domain.Seller.Seller seller = await SellerManager.GetSellerById(SellerId);

            var Statistic = await _statisticDomainService.GetSellerStatistic(seller.Id);

            return new PublicDetailSellerDto
            {
                Id = seller.Id,
                CoverUrl = seller.SellerCover?.Image.Url,
                Description = seller.Description,
                LogoUrl = seller.SellerLogo?.Image.Url,
                Name = seller.Name,
                AuctionCount = Statistic.AuctionCount,
                OrderCount = Statistic.OrderCount
            };
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<SellerDetailDto> GetSellerInfo(long SellerId)
        {
            Domain.Seller.Seller seller = await SellerManager.GetSellerById(SellerId);
            return new SellerDetailDto
            {
                Id = seller.Id,
                Name = seller.Name,
                Address = seller.Address,
                Description = seller.Description,
                EmailAddress = seller.EmailAddress,
                PaymentRegisterStatus = seller.PaymentRegisterStatus,
                PhoneNumber = seller.PhoneNumber,
                SellerCoverId = seller.SellerCoverId,
                SellerCoverUrl = seller.SellerCover?.Image?.Url,
                SellerLogoId = seller.SellerLogoId,
                SellerLogoUrl = seller.SellerLogo?.Image?.Url,
                UserId = seller.UserId,
                UserName = seller.User.UserName
            };
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<SellerPaymentDto> GetPaymenSellerInfo(long SellerId)
        {
            Domain.Seller.SellerPaymentOption payment = await SellerManager.GetSellerPaymentOption(SellerId);
            return new SellerPaymentDto
            {
                Payload = payment.GetKeyValuePairData(),
                SellerPaymentOption = payment.PaymentOption
            };
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<SellerPaymentDto> GetYourPaymenSellerInfo()
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            Domain.Seller.SellerPaymentOption payment = await SellerManager.GetSellerPaymentOption(seller.Id);
            return new SellerPaymentDto
            {
                Payload = payment.GetKeyValuePairData(),
                SellerPaymentOption = payment.PaymentOption
            };
        }


        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<PagedResultDto<SellerDto>> GetAllSellers(PagedSellerRequestDto input)
        {
            IQueryable<SellerDto> sellers = (await SellerManager
                .SearchSeller(input.Keyword))
                .Select(s =>
                new SellerDto
                {
                    Id = s.Id,
                    Address = s.Address,
                    Name = s.Name,
                    SellerLogoUrl = s.SellerLogo.Image.Url,
                    UserName = s.User.UserName
                })
                ;

            return await GetPagedResult(sellers, input);
        }

        public async Task<PagedResultDto<PublicSellerDto>> GetAllPublicSellers(PagedSellerRequestDto input)
        {
            IQueryable<PublicSellerDto> sellers = (await SellerManager
                .SearchSeller(input.Keyword))
                .Select(s =>
                new PublicSellerDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    LogoUrl = s.SellerLogo.Image.Url
                });

            return await GetPagedResult(sellers, input);
        }

    }
}
