using System.Threading.Tasks;
using Abp.Application.Services;
using WebShop.Sessions.Dto;

namespace WebShop.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
