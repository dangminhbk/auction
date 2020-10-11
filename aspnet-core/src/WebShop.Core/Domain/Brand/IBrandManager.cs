using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Brand
{
    public interface IBrandManager : IDomainService
    {
        Task CreateBrand();
        Task EditBrand();
        Task DeleteBrand();
        Task<Brand> GetAllBrand();
        Task GetBrand();
    }
}
