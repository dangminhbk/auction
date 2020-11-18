using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebShop.Authorization;

namespace WebShop
{
    [DependsOn(
        typeof(WebShopCoreModule),
        typeof(AbpAutoMapperModule))]
    public class WebShopApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<WebShopAuthorizationProvider>();
        }

        public override void Initialize()
        {
            System.Reflection.Assembly thisAssembly = typeof(WebShopApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
