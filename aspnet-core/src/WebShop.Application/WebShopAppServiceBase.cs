using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using WebShop.Authorization.Users;
using WebShop.MultiTenancy;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using WebShop.Domain.Seller;
using Abp.Dependency;

namespace WebShop
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class WebShopAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public ISellerManager SellerManager { get; set; }

        protected WebShopAppServiceBase()
        {
            LocalizationSourceName = WebShopConsts.LocalizationSourceName;
            //this.SellerManager = IocManager.Instance.Resolve<ISellerManager>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual async Task<Domain.Seller.Seller> GetCurrentSeller()
        {
            var userId = (await GetCurrentUserAsync()).Id;
            var seller = await SellerManager.
                                GetSellerByUserId(userId);
            if (seller == null)
            {
                throw new Exception("Cannot get seller");
            }
            return seller;
        }

        protected virtual async Task<PagedResultDto<T>> GetPagedResult<T>(IQueryable<T> query, IPagedResultRequest input) where T : class
        {
            var items = ApplyPaging<T>(query, input);
            var count = await query.CountAsync();
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
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            //Try to limit query result if available
            var limitedInput = input as ILimitedResultRequest;
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
