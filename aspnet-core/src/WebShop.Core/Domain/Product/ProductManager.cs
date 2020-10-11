using Abp.Domain.Repositories;
using Abp.Domain.Services;
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
        public async Task CreateProduct(Product product)
        {
            await ProductRepository.InsertAsync(product);
            await Commit();
        }

        public async Task DeleteProduct(Product product)
        {
            await ProductRepository.DeleteAsync(product);
            await Commit();
        }
        public async Task UpdateProduct(Product product)
        {
            await ProductRepository.UpdateAsync(product);
            await Commit();
        }

        public Task<IQueryable<Product>> GetAll()
        {
            throw new NotImplementedException();
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

        public Task<IQueryable<Product>> GetAllForSeller(long sellerId)
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
    }
}
