using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Product
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IRepository<Product, long> ProductRepository;
        public ProductManager(IRepository<Product, long> productRepository)
        {
            ProductRepository = productRepository;
        }
        public async Task CreateProduct(
            string Name,
            decimal Price,
            string Description,
            long? CoverImageId,
            long[] ProductImages,
            long SellerId,
            long? BrandId,
            long[] ProductCategories
        )
        {
            var product = new Product
            {
                Name = Name,
                BrandId = BrandId,
                SellerId = SellerId,
                Description = Description,
                Price = Price
            };

            if (CoverImageId.HasValue)
            {
                var cover = new ProductCoverImage
                {
                    ImageId = CoverImageId.Value
                };

                product.CoverImage = cover;
            }

            product.ProductImages = new List<ProductImage>();

            foreach (var image in ProductImages)
            {
                var productImage = new ProductImage
                {
                    ImageId = image
                };

                product.ProductImages.Add(productImage);
            }

            await this.ProductRepository.InsertAsync(product);
        }

        public async Task DeleteProduct(long id, long sellerId)
        {

        }
        public async Task UpdateProduct(
            long Id,
            string Name,
            decimal Price,
            string Description,
            long? CoverImageId,
            long[] ProductImages,
            long SellerId,
            long? BrandId,
            long[] ProductCategories)
        {
        }

        public async Task<IQueryable<Product>> GetAll(
            string keyword, 
            decimal? minPrice, 
            decimal? maxPrice,
            DateTime? minCreateDate,
            DateTime? maxCreateDate,
            long? brandId,
            long[] categories
            )
        {
            var result = this.ProductRepository
                .GetAll()
                .Include(s => s.ProductImages)
                .ThenInclude(s => s.Image)
                .Include(s => s.CoverImage)
                .ThenInclude(s => s.Image)
                .Include(s => s.Brand);

            return result;
        }

        public Task<IQueryable<Product>> GetAllByBrand(long brandId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> GetAllByCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> GetAllForBuyer()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> SearchForBuyer(string searchText)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Product>> SearchForSeller(long sellerId, string searchText)
        {
            throw new NotImplementedException();
        }

        private async Task Commit()
        {
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public Task<IQueryable<Product>> GetAll(string keyword, decimal minPrice, decimal maxPrice, DateTime minCreateDate, DateTime maxCreateDate, long brandId, long[] categories)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> Get(long id, long sellerId)
        {
            var product = this.ProductRepository
                .GetAll()
                .Include(s => s.ProductImages)
                .ThenInclude(s => s.Image)
                .Include(s => s.CoverImage)
                .ThenInclude(s => s.Image)
                .Include(s => s.Brand)
                .FirstOrDefault(s=>s.Id == id);
            return product;
        }

        public async Task<IQueryable<Product>> GetAllForSeller(long sellerId, string keyword)
        {
            return (await this.GetAll(keyword,null, null, null, null, null, null))
                .Where(s=>s.SellerId == sellerId);
        }
    }
}
