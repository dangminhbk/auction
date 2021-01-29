using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Domain.Image;
using WebShop.Product;
using WebShop.ProductAdmin.Dto;

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

            Domain.Seller.Seller seller = await GetCurrentSeller();
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
        [HttpPost]
        public async Task Edit(ProductEditDto input)
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            // Check images asset

            //
            await ProductManager.UpdateProduct(
                input.Id,
                input.Name,
                input.Price,
                input.Description,
                input.CoverImageId,
                input.ImageIds,
                input.BrandId,
                input.CategoryIds
                );

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<PagedResultDto<ProductListDto>> GetAllSeller(PagedProductRequestDto input)
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            IQueryable<Product.Product> result = await ProductManager.GetAllForSeller(seller.Id, input.Keyword);

            IQueryable<ProductListDto> mapped = result.Select(s => new ProductListDto
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
            //var seller = await GetCurrentSeller();
            Product.Product product = await ProductManager.Get(input.Id, 0);
            System.Collections.Generic.IEnumerable<ImageDto> images = product.ProductImages.Select(s => new ImageDto
            {
                Id = s.Image.Id,
                Url = s.Image.Url
            });

            return new ProductSellerDetail
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CoverImageUrl = product.CoverImage?.Image?.Url,
                CoverImageId = product.CoverImage?.Image?.Id,
                Description = product.Description,
                ImageIds = product.ProductImages?.Where(s => s.Image != null).Select(s => s.Image.Id).ToArray(),
                ImageUrls = product.ProductImages?.Where(s => s.Image != null).Select(s => s.Image.Url).ToArray(),
                Name = product.Name,
                Price = product.Price,
                SellerId = product.SellerId,
                Images = images.ToList(),
                CategoryIds = product.ProductCategories?.Select(s => s.CategoryId).ToArray()
            };
        }

        [HttpDelete]
        public async Task Delete(EntityDto<long> input)
        {

        }
    }
}
