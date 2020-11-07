using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using WebShop.Category;
using WebShop.Domain.Brand;
using WebShop.Domain.Image;
using WebShop.Domain.Seller;

namespace WebShop.Product
{
    public class Product : FullAuditedAggregateRoot<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(ProductCoverImage))]
        public long? CoverImageId { get; set; }
        public ProductCoverImage CoverImage { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        [ForeignKey(nameof(Product.Seller))]
        public long SellerId { get; set; }
        public virtual Seller Seller {get; set;}
        [ForeignKey(nameof(Product.Brand))]
        public long? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories  { get; set; }
        public ProductState State { get; set; }
    }

    public enum ProductState
    {
        InActive, Active
    }

    public class ProductCategory : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProductCategory.Product))]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(ProductCategory.Category))]
        public long CategoryId { get; set; }
        public Category.Category Category { get; set; }
    }

    public class ProductImage : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProductImage.Product))]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey(nameof(ProductImage.Image))]
        public long ImageId { get; set; }
        public Image Image { get; set; }
    }

    public class ProductCoverImage: FullAuditedEntity<long>
    {
        [ForeignKey(nameof(ProductImage.Image))]
        public long ImageId { get; set; }
        public Image Image { get; set; }
    }
}
