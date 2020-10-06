using System.Threading.Tasks;
using WebShop.Configuration.Dto;

namespace WebShop.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
