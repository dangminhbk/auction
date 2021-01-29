using Abp.Configuration;
using System.Collections.Generic;

namespace WebShop.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.MainBannerImage, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.MainBannerLink, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.MainBannerText, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.MainBannerHeadLine, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SeccondBannerImage, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SeccondBannerLink, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SeccondBannerText, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.SeccondBannerHeadLine, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ThirdBannerImage, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ThirdBannerLink, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ThirdBannerText, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ThirdBannerHeadLine, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.PromoImage, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.PromoLink, "", scopes: SettingScopes.Application, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.PromoOn, "false", scopes: SettingScopes.Application, isVisibleToClients: true),
            };
        }
    }
}
