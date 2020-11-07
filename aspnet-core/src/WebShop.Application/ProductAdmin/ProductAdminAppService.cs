using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Image;
using WebShop.Product;
using WebShop.ProductAdmin.Dto;
using System.Linq;

namespace WebShop.ProductAdmin
{
    public class ProductAdminAppService : WebShopAppServiceBase
    {
        private readonly IProductManager ProductManager;
        private readonly IImageManager ImageManager; 
        public ProductAdminAppService(
            IProductManager productManager, 
            IImageManager imageManager)
        {
            ProductManager = productManager;
            ImageManager = imageManager;
        }
        public async Task Create(ProductCreateDto input)
        {
            // Get seller info

            var seller = await GetCurrentSeller();
            // Check images asset

            //
            await ProductManager.CreateProduct(
                input.Name,
                input.Price,
                input.Description,
                input.CoverImageId,
                input.ImageIds,
                seller.Id,
                input.BrandId,
                input.CategoryIds
                );

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultDto<ProductListDto>> GetAllSeller(PagedProductRequestDto input)
        {
            var seller = await GetCurrentSeller();
            var result = await ProductManager.GetAllForSeller(seller.Id, input.Keyword);

            var mapped = result.Select(s=> new ProductListDto
            {
                Id = s.Id,
                CoverImageUrl = s.CoverImage.Image.Url,
                Name = s.Name,
                Price = s.Price,
                CreateDate = s.CreationTime
            });

            return await GetPagedResult(mapped, input);
        }

        public async Task<ProductSellerDetail> Get(EntityDto<long> input)
        {
            var seller = await GetCurrentSeller();
            var product = await ProductManager.Get(input.Id, seller.Id);

            return new ProductSellerDetail
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryIds = product.ProductCategories.Select(s=>s.CategoryId).ToArray(),
                CoverImageUrl = product.CoverImage.Image.Url,
                CoverImageId = product.CoverImage.Image.Id,
                Description = product.Description,
                ImageIds = product.ProductImages.Select(s=>s.Image.Id).ToArray(),
                ImageUrls = product.ProductImages.Select(s=>s.Image.Url).ToArray(),
                Name = product.Name,
                Price = product.Price,
                SellerId = product.SellerId
            };
        }

        [HttpDelete]
        public async Task Delete(EntityDto<long> input)
        {

        }
    }
}
