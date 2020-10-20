using Abp.Dependency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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
            var dbContext = IocManager.Instance.Resolve<TDbContext>();
            if (!dbContext.Database.IsMySql())
            {
                throw new Exception("Operation is not support");
            }

            var propertyName = property.Name;
            var tableName = GetTableName<T>(dbContext);

            var query = dbSet.FromSqlRaw(
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
            var models = context.Model;

            // Get all the entity types information
            var entityTypes = models.GetEntityTypes();

            // T is Name of class
            var entityTypeOfT = entityTypes.First(t => t.ClrType == typeof(T));

            var tableNameAnnotation = entityTypeOfT.GetAnnotation("Relational:TableName");
            var TableName = tableNameAnnotation.Value.ToString();
            return TableName;

        }
    }
}

