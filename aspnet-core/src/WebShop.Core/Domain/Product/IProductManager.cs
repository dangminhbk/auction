using Abp.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Product
{
    public interface IProductManager : IDomainService
    {
        #region Seller

        Task CreateProduct(
            string Name,
            decimal Price,
            string Description,
            long? CoverImageId,
            long[] ProductImages,
            long SellerId,
            long? BrandId,
            long[] ProductCategories
        );
        Task UpdateProduct(
            long Id,
            string Name,
            decimal Price,
            string Description,
            long? CoverImageId,
            long[] ProductImages,
            long? BrandId,
            long[] ProductCategories
         );

        Task DeleteProduct(long id, long sellerId);

        Task<IQueryable<Product>> GetAll(
            string keyword,
            decimal? minPrice,
            decimal? maxPrice,
            DateTime? minCreateDate,
            DateTime? maxCreateDate,
            long? brandId,
            long[] categories
        );

        Task<Product> Get(long id, long sellerId);

        Task<IQueryable<Product>> GetAllForSeller(
            long sellerId,
            string keyword
        );

        #endregion

        #region Buyer

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
