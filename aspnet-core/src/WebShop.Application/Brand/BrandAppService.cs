using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Brand.Dto;
using WebShop.Domain.Brand;

namespace WebShop.Brand
{
    public class BrandAppService : WebShopAppServiceBase
    {
        private readonly IBrandManager BrandManager;
        public BrandAppService(IBrandManager brandManager)
        {
            BrandManager = brandManager;
        }

        public async Task Create(CreateBrandDto input)
        {
            await BrandManager.CreateBrand(
                    input.Name,
                    input.BrandImageId,
                    input.Description
            );
        }

        public async Task Delete(EntityDto<long> input)
        {
            await BrandManager.DeleteBrand(input.Id);
        }

        public async Task Edit(EditBrandDto input)
        {
            await BrandManager.EditBrand(
                input.Id,
                input.Name,
                input.BrandImageId,
                input.Description
            );
        }

        public async Task<PagedResultDto<BrandDto>> GetAll(PagedBrandRequestDto input)
        {
            var raw = await BrandManager.GetAllBrand(input.Keyword);
            var result = raw.Select(s => new BrandDto
            {
                BrandImageId = s.BrandImage.Image.Id,
                BrandImageUrl = s.BrandImage.Image.Url,
                Description = s.Description,
                Id = s.Id,
                Name = s.Name
            });

            return await GetPagedResult<BrandDto>(result, input);
        }

        public async Task<List<BrandDto>> GetDropdown()
        {
            var raw = await BrandManager.GetAllBrand(null);
            var result = raw.Select(s => new BrandDto
            {
                BrandImageId = s.BrandImage.Image.Id,
                BrandImageUrl = s.BrandImage.Image.Url,
                Description = s.Description,
                Id = s.Id,
                Name = s.Name
            });

            return result.ToList();
        }

        public async Task<BrandDto> Get(long id)
        {
            var raw = await BrandManager.GetBrand(id);
            return new BrandDto
            {
                Id = raw.Id,
                BrandImageId = raw.BrandImage?.Image?.Id,
                BrandImageUrl = raw.BrandImage?.Image?.Url,
                Description = raw.Description,
                Name = raw.Name
            };
        }
    }
}
