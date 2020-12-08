using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Authorization;

namespace WebShop.Domain.Brand
{
    public class BrandManager : DomainService, IBrandManager
    {
        private readonly IRepository<Brand, long> Repository;
        public BrandManager(IRepository<Brand, long> repository)
        {
            Repository = repository;
        }
        [AbpAuthorize(nameof(PermissionNames.Admins))]
        public async Task CreateBrand(string BrandName, long? BrandImageId, string Description)
        {
            Brand brand = new Brand
            {
                Name = BrandName,
                Description = Description
            };

            if (BrandImageId.HasValue)
            {
                BrandImage brandImage = new BrandImage
                {
                    ImageId = BrandImageId.Value
                };

                brand.BrandImage = brandImage;
            }

            await Repository.InsertAsync(brand);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(nameof(PermissionNames.Admins))]
        public async Task DeleteBrand(long BrandId)
        {
            await Repository.DeleteAsync(BrandId);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(nameof(PermissionNames.Admins))]
        public async Task EditBrand(long BrandId, string BrandName, long? BrandImage, string Description)
        {
            Brand brand = await GetBrand(BrandId);
            if (BrandImage.HasValue)
            {
                Image.Image oldImage = brand.BrandImage.Image;
                if (oldImage.Id != BrandImage)
                {
                    BrandImage brandImage = new BrandImage
                    {
                        ImageId = BrandImage.Value
                    };
                    brand.BrandImage = brandImage;
                }
            }
            brand.Description = Description;
            brand.Name = BrandName;
            await Repository.UpdateAsync(brand);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        //[AbpAuthorize(nameof(PermissionNames.Admins))]
        public async Task<IQueryable<Brand>> GetAllBrand(string Keyword)
        {
            return Repository
                .GetAllIncluding(s => s.BrandImage, s => s.BrandImage.Image)
                .WhereIf(!Keyword.IsNullOrEmpty(), s => s.Name.Contains(Keyword));
        }

        //[AbpAuthorize(nameof(PermissionNames.Admins))]
        public async Task<Brand> GetBrand(long BrandId)
        {
            Brand result = await Repository
                .GetAllIncluding(s => s.BrandImage, s => s.BrandImage.Image)
                .FirstOrDefaultAsync(s => s.Id == BrandId);
            if (result == null)
            {
                throw new Exception("Entity not found!");
            }
            return result;
        }
    }
}
