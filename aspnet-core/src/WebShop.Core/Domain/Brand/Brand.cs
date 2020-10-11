using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebShop.Domain.Brand
{
    public class Brand : FullAuditedAggregateRoot<long>
    {
        public string Name { get; set; }
       // public virtual ICollection<Product.Product> Products { get; set; }
        [ForeignKey(nameof(BrandImage))]
        public long? BrandImageId { get; set; }
        public BrandImage BrandImage { get; set; }
    }

    public class BrandImage : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(Image))]
        public long ImageId { get; set; }
        public Image.Image Image { get; set; }
    }
}
