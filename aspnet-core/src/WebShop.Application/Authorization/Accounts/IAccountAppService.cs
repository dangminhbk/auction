using Abp.Application.Services;
using System.Threading.Tasks;
using WebShop.Authorization.Accounts.Dto;

namespace WebShop.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
