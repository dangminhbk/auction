using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Domain.Seller;
using WebShop.Extension;
using WebShop.Seller.Dto;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Seller
{
    public class SellerAppService : WebShopAppServiceBase
    {
        public SellerAppService()
        {
        }

        [HttpPut]
        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task UpdateSellerInfo(UpdateSellerDto input)
        {
            var sellerId = (await GetCurrentSeller()).Id;
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
            var sellerId = (await GetCurrentSeller()).Id;
            await SellerManager.UpdatePayment(
                sellerId,
                input.Payload,
                input.SellerPaymentOption
            );
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<SellerDetailDto> GetYourSellerInfo()
        {
            var seller = await GetCurrentSeller();
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
                UserName = seller.User.UserName
            };
        }

        public async Task<PublicDetailSellerDto> GetPublicSellerInfo(long SellerId)
        {
            var seller = await SellerManager.GetSellerById(SellerId);
            return new PublicDetailSellerDto
            {
                Id = seller.Id,
                CoverUrl = seller.SellerCover?.Image.Url,
                Description = seller.Description,
                LogoUrl = seller.SellerLogo?.Image.Url,
                Name = seller.Name
            };
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<SellerDetailDto> GetSellerInfo(long SellerId)
        {
            var seller = await SellerManager.GetSellerById(SellerId);
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
            var payment = await SellerManager.GetSellerPaymentOption(SellerId);
            return new SellerPaymentDto
            {
                Payload = payment.GetKeyValuePairData(),
                SellerPaymentOption = payment.PaymentOption
            };
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<SellerPaymentDto> GetYourPaymenSellerInfo()
        {
            var seller = await GetCurrentSeller();
            var payment = await SellerManager.GetSellerPaymentOption(seller.Id);
            return new SellerPaymentDto
            {
                Payload = payment.GetKeyValuePairData(),
                SellerPaymentOption = payment.PaymentOption
            };
        }


        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<PagedResultDto<SellerDto>> GetAllSellers(PagedSellerRequestDto input)
        {
            var sellers = (await SellerManager
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
            var sellers = (await SellerManager
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
