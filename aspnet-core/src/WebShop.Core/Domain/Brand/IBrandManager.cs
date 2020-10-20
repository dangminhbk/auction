using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Brand
{
    public interface IBrandManager : IDomainService
    {
        Task CreateBrand(string BrandName, long? BrandImage, string Description);
        Task EditBrand(long BrandId, string BrandName, long? BrandImage, string Description);
        Task DeleteBrand(long BrandId);
        Task<IQueryable<Brand>> GetAllBrand(string Keyword);
        Task<Brand>  GetBrand(long BrandId);
    }
}
