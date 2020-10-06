using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WebShop.Authorization.Roles;
using WebShop.Authorization.Users;
using WebShop.MultiTenancy;
using WebShop.Product;

namespace WebShop.EntityFrameworkCore
{
    public class WebShopDbContext : AbpZeroDbContext<Tenant, Role, User, WebShopDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<Seller.Seller> Sellers { get; set; }
        public DbSet<Brand.Brand> Brands { get; set; }
        public DbSet<Category.Category> Categories { get; set; }
        public DbSet<Image.Image> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCoverImage> ProductCoverImages { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }
    }
}
