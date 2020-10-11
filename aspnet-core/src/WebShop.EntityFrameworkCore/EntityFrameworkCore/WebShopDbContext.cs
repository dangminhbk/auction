using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WebShop.Authorization.Roles;
using WebShop.Authorization.Users;
using WebShop.MultiTenancy;
using WebShop.Product;
using WebShop.Domain.Brand;
using WebShop.Domain.Image;

namespace WebShop.EntityFrameworkCore
{
    public class WebShopDbContext : AbpZeroDbContext<Tenant, Role, User, WebShopDbContext>
    {
        /* Define a DbSet for each entity of the application */
        // Product
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCoverImage> ProductCoverImages { get; set; }
        // Seller
        public DbSet<Seller.Seller> Sellers { get; set; }
        // Brand
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandImage> BrandImages {get; set;}
        // Category
        public DbSet<Category.Category> Categories { get; set; }
        // Image
        public DbSet<Image> Images { get; set; }


        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }
    }
}
