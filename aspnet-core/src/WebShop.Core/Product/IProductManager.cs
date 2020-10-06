using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Product
{
    public interface IProductManager : IDomainService
    {
        #region Seller
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task CreateProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task UpdateProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task DeleteProduct(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Product>> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> GetAllForSeller(long sellerId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> SearchForSeller(long sellerId, string searchText);
        #endregion

        #region Buyer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Product>> GetAllForBuyer();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> SearchForBuyer(string searchText);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> GetAllByBrand(long brandId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> GetAllByCategory(long categoryId);
        #endregion
    }
}
