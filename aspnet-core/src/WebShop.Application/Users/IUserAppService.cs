using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using WebShop.Roles.Dto;
using WebShop.Users.Dto;

namespace WebShop.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
