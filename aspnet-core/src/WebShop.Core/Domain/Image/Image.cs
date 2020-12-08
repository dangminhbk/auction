using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Domain.Image
{
    public class Image : FullAuditedAggregateRoot<long>
    {
        public string Identified { get; set; }
        public string Url { get; set; }
        [ForeignKey(nameof(Image.Seller))]
        public long? SellerId { get; set; }
        public virtual Seller.Seller Seller { get; set; }
    }
}
