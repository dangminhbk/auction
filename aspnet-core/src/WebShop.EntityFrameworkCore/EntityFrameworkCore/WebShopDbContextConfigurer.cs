using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace WebShop.EntityFrameworkCore
{
    public static class WebShopDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<WebShopDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<WebShopDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
