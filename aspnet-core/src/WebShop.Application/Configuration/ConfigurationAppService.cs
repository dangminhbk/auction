using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using WebShop.Configuration.Dto;

namespace WebShop.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : WebShopAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
