using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebShop.EntityFrameworkCore;
using WebShop.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace WebShop.Web.Tests
{
    [DependsOn(
        typeof(WebShopWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class WebShopWebTestModule : AbpModule
    {
        public WebShopWebTestModule(WebShopEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebShopWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(WebShopWebMvcModule).Assembly);
        }
    }
}