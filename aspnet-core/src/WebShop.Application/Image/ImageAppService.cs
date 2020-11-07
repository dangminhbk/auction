using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Domain.Image.Dto;

namespace WebShop.Domain.Image
{
    public class ImageAppService : WebShopAppServiceBase
    {
        private readonly IImageManager ImageManager;
        private readonly IRepository<Seller.Seller, long> SellerRepository;
        public ImageAppService(IImageManager imageManager, IRepository<Seller.Seller, long> sellerRepository)
        {
            ImageManager = imageManager;
            SellerRepository = sellerRepository;
        }

        [AbpAuthorize(nameof(PermissionNames.Admins))]
        [HttpPost]
        public async Task<long[]> Upload([FromForm] ImageUploadDto input)
        {
            return await ImageManager.UploadImages(null, input.Files);
        }

        [AbpAuthorize(nameof(PermissionNames.Sellers))]
        [HttpPost]
        public async Task<long[]> UploadSeller([FromForm] ImageUploadDto input)
        {
            var seller = await GetCurrentSeller();
            return await ImageManager.UploadImages(seller.Id, input.Files);
        }

        [AbpAuthorize(PermissionNames.Admins,PermissionNames.Sellers)]
        public async Task<string> UploadCK([FromForm] ImageUploadDto input)
        {
            var seller = await GetCurrentSeller();
            var url = await ImageManager.UploadWithResult(seller?.Id, input.Files[0]);
            return url;
        }

        [AbpAuthorize(nameof(PermissionNames.Admins))]
        [HttpGet]
        public async Task<PagedResultDto<ImageListDto>> GetAll(PagedResultRequestDto input)
        {
            var images = await ImageManager.GetSystemImages();
            var imageResult = images.Select(s => new ImageListDto {
                Id = s.Id,
                CreationTime = s.CreationTime,
                Identified = s.Identified,
                Url = s.Url
            });
            return await GetPagedResult(imageResult, input);
        }

        [AbpAuthorize(nameof(PermissionNames.Sellers))]
        [HttpGet]
        public async Task<PagedResultDto<ImageListDto>> GetAllSeller(PagedResultRequestDto input)
        {
            var seller = await GetCurrentSeller();
            var images = await ImageManager.GetImagesForSeller(seller.Id);
            var imageResult = images.Select(s => new ImageListDto
            {
                Id = s.Id,
                CreationTime = s.CreationTime,
                Identified = s.Identified,
                Url = s.Url
            });
            return await GetPagedResult(imageResult, input);
        }

        [AbpAuthorize(nameof(PermissionNames.Admins))]
        [HttpDelete]
        public async Task DeleteSystem(EntityDto<long> input)
        {
            await ImageManager.DeleteImages(input.Id);
        }

        [AbpAuthorize(nameof(PermissionNames.Sellers))]
        [HttpDelete]
        public async Task DeleteSeller(EntityDto<long> input)
        {
            await ImageManager.DeleteImages(input.Id);
        }

    }
}
