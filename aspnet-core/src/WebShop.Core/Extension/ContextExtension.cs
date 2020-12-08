using Abp.Dependency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebShop.Extension
{
    public static class ContextExtension
    {
        public static IQueryable<T> FullTextSearch<T, TDbContext>(
            this DbSet<T> dbSet,
            PropertyInfo property,
            string searchText
        )
        where T : class
        where TDbContext : DbContext
        {
            TDbContext dbContext = IocManager.Instance.Resolve<TDbContext>();
            if (!dbContext.Database.IsMySql())
            {
                throw new Exception("Operation is not support");
            }

            string propertyName = property.Name;
            string tableName = GetTableName<T>(dbContext);

            IQueryable<T> query = dbSet.FromSqlRaw(
                @"SELECT *
                FROM {0} MATCH ({1})
                AGAINST ('{2}' IN NATURAL LANGUAGE)",
                    tableName,
                    propertyName,
                    searchText);

            return query;
        }

        public static string GetTableName<T>(DbContext context) where T : class
        {
            // We need dbcontext to access the models
            Microsoft.EntityFrameworkCore.Metadata.IModel models = context.Model;

            // Get all the entity types information
            IEnumerable<Microsoft.EntityFrameworkCore.Metadata.IEntityType> entityTypes = models.GetEntityTypes();

            // T is Name of class
            Microsoft.EntityFrameworkCore.Metadata.IEntityType entityTypeOfT = entityTypes.First(t => t.ClrType == typeof(T));

            IAnnotation tableNameAnnotation = entityTypeOfT.GetAnnotation("Relational:TableName");
            string TableName = tableNameAnnotation.Value.ToString();
            return TableName;

        }
    }
}

