using Abp.Application.Services;
using WebShop.MultiTenancy.Dto;

namespace WebShop.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

