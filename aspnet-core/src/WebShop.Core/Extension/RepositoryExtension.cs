using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace WebShop.Extension
{
    public static class RepositoryExtension
    {
        public static IQueryable<T> FullTextSearch<T, TPrimaryKey>(
            this IRepository<T, TPrimaryKey> repository,
            PropertyInfo property,
            string text
            )
            where T : class, IEntity<TPrimaryKey>
        {
            DbContext context = repository.GetDbContext();
            return context.Set<T>().FullTextSearch<T, AbpDbContext>(property, text);
        }
    }
}
