using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

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
