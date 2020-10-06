using System.Threading.Tasks;
using Abp.Application.Services;
using WebShop.Authorization.Accounts.Dto;

namespace WebShop.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
