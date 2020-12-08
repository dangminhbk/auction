using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;
using WebShop.EntityFrameworkCore.Seed.Host;
using WebShop.EntityFrameworkCore.Seed.Tenants;

namespace WebShop.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<WebShopDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(WebShopDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            // Host seed
            new InitialHostDbBuilder(context).Create();

            // Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (IDisposableDependencyObjectWrapper<IUnitOfWorkManager> uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (IUnitOfWorkCompleteHandle uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    TDbContext context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
