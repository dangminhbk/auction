using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WebShop.Authorization.Roles;
using WebShop.Authorization.Users;
using WebShop.MultiTenancy;
using WebShop.Product;
using WebShop.Domain.Brand;
using WebShop.Domain.Image;
using WebShop.Domain.Seller;
using WebShop.Domain.Auction;
using WebShop.Domain.Bid;
using WebShop.Domain.Invoice;

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
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerLogo> SellerLogos { get; set; }
        public DbSet<SellerLogo> SellerCover { get; set; }
        public DbSet<SellerPaymentOption> SellerPaymentOptions { get; set; }
        // Brand
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandImage> BrandImages {get; set;}
        // Category
        public DbSet<Category.Category> Categories { get; set; }
        // Image
        public DbSet<Image> Images { get; set; }

        // Auction
        public DbSet<Auction> Auctions { get; set; }

        // Bid
        public DbSet<Bid> Bids { get; set; }

        // Invoice
        public DbSet<Invoice> Invoices { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }
    }
}
