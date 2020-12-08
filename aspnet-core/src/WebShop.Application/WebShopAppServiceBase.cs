using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Authorization.Users;
using WebShop.Domain.Seller;
using WebShop.MultiTenancy;

namespace WebShop
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class WebShopAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected ISellerManager SellerManager { get; set; }

        protected WebShopAppServiceBase()
        {
            LocalizationSourceName = WebShopConsts.LocalizationSourceName;
            SellerManager = IocManager.Instance.Resolve<ISellerManager>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            User user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual async Task<Domain.Seller.Seller> GetCurrentSeller()
        {
            long userId = (await GetCurrentUserAsync()).Id;
            Domain.Seller.Seller seller = await SellerManager.
                                GetSellerByUserId(userId);
            if (seller == null)
            {
                return null;
            }
            return seller;
        }

        protected virtual async Task<PagedResultDto<T>> GetPagedResult<T>(IQueryable<T> query, IPagedResultRequest input) where T : class
        {
            IQueryable<T> items = ApplyPaging<T>(query, input);
            int count = await query.CountAsync();
            return new PagedResultDto<T>(count, await items.ToListAsync());
        }

        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplyPaging<TEntity>(IQueryable<TEntity> query, IPagedResultRequest input)
        {
            //Try to use paging if available
            IPagedResultRequest pagedInput = input;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            //Try to limit query result if available
            ILimitedResultRequest limitedInput = input;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            //No paging
            return query;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
