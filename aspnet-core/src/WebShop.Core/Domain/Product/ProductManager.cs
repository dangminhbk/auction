using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Product product = new Product
            {
                Name = Name,
                BrandId = BrandId,
                SellerId = SellerId,
                Description = Description,
                Price = Price
            };

            if (CoverImageId.HasValue)
            {
                ProductCoverImage cover = new ProductCoverImage
                {
                    ImageId = CoverImageId.Value
                };

                product.CoverImage = cover;
            }

            product.ProductCategories = new List<ProductCategory>();

            foreach (long category in ProductCategories)
            {
                ProductCategory productCategory = new ProductCategory
                {
                    CategoryId = category
                };

                product.ProductCategories.Add(productCategory);
            }

            product.ProductImages = new List<ProductImage>();

            foreach (long image in ProductImages)
            {
                ProductImage productImage = new ProductImage
                {
                    ImageId = image
                };

                product.ProductImages.Add(productImage);
            }

            await ProductRepository.InsertAsync(product);
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
            long? BrandId,
            long[] ProductCategories)
        {
            Product product = await Get(Id, 0);
            product.Name = Name;
            product.Price = Price;
            product.Description = Description;
            product.BrandId = BrandId;
            if (product.CoverImageId.HasValue && product.CoverImage.ImageId != CoverImageId.Value)
            {
                product.CoverImage = new ProductCoverImage
                {
                    ImageId = CoverImageId.Value
                };
            }

            if (CoverImageId.HasValue)
            {
                ProductCoverImage cover = new ProductCoverImage
                {
                    ImageId = CoverImageId.Value
                };

                product.CoverImage = cover;
            }

            product.ProductCategories = new List<ProductCategory>();

            foreach (long category in ProductCategories)
            {
                ProductCategory productCategory = new ProductCategory
                {
                    CategoryId = category
                };

                product.ProductCategories.Add(productCategory);
            }

            product.ProductImages = new List<ProductImage>();

            foreach (long image in ProductImages)
            {
                ProductImage productImage = new ProductImage
                {
                    ImageId = image
                };

                product.ProductImages.Add(productImage);
            }

            await ProductRepository.UpdateAsync(product);

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
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, Domain.Brand.Brand> result = ProductRepository
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
            Product product = ProductRepository
                .GetAll()
                .Include(s => s.ProductImages)
                .ThenInclude(s => s.Image)
                .Include(s => s.CoverImage)
                .ThenInclude(s => s.Image)
                .Include(s => s.Brand)
                .Include(s => s.ProductCategories)
                .ThenInclude(s => s.Category)
                .FirstOrDefault(s => s.Id == id);
            return product;
        }

        public async Task<IQueryable<Product>> GetAllForSeller(long sellerId, string keyword)
        {
            return (await GetAll(keyword, null, null, null, null, null, null))
                .Where(s => s.SellerId == sellerId);
        }
    }
}
