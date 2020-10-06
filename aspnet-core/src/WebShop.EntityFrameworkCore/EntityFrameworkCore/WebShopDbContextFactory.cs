using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebShop.Configuration;
using WebShop.Web;

namespace WebShop.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class WebShopDbContextFactory : IDesignTimeDbContextFactory<WebShopDbContext>
    {
        public WebShopDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WebShopDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            WebShopDbContextConfigurer.Configure(builder, configuration.GetConnectionString(WebShopConsts.ConnectionStringName));

            return new WebShopDbContext(builder.Options);
        }
    }
}
