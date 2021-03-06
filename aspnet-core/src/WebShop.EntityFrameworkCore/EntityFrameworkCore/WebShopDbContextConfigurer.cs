using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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
