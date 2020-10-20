using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShop.Domain.Image;

namespace WebShop.Domain.Brand
{
    public class Brand : FullAuditedAggregateRoot<long>
    {
        public string Name { get; set; }
       // public virtual ICollection<Product.Product> Products { get; set; }
        [ForeignKey(nameof(BrandImage))]
        public long? BrandImageId { get; set; }
        public virtual BrandImage BrandImage { get; set; }
        public string Description { get; set; } 
    }

    public class BrandImage : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(Image))]
        public long ImageId { get; set; }
        public Image.Image Image { get; set; }
    }
}
