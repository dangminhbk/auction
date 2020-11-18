using Abp.Application.Services;
using System.Threading.Tasks;
using WebShop.Sessions.Dto;

namespace WebShop.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
