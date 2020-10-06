using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace WebShop.Authorization
{
    public class WebShopAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Admins, L("Admins"));
            context.CreatePermission(PermissionNames.Buyers, L("Buyers"));
            context.CreatePermission(PermissionNames.Sellers, L("Sellers"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, WebShopConsts.LocalizationSourceName);
        }
    }
}
