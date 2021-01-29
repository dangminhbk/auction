using Abp.Authorization;
using Abp.Runtime.Session;
using System.Threading.Tasks;
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

        public async Task ChangeSetting(ChangeSettingDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MainBannerHeadLine, input.MainBanner.Headline);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MainBannerImage, input.MainBanner.Image);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MainBannerLink, input.MainBanner.Link);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MainBannerText, input.MainBanner.Text);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SeccondBannerHeadLine, input.SecondBanner.Headline);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SeccondBannerImage, input.SecondBanner.Image);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SeccondBannerLink, input.SecondBanner.Link);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SeccondBannerText, input.SecondBanner.Text);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ThirdBannerHeadLine, input.ThirdBanner.Headline);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ThirdBannerImage, input.ThirdBanner.Image);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ThirdBannerLink, input.ThirdBanner.Link);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ThirdBannerText, input.ThirdBanner.Text);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.PromoImage, input.Promo.Image);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.PromoLink, input.Promo.Link);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.PromoOn, input.Promo.IsPromo.ToString().ToLower());
        }
    }
}
