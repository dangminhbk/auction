using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebShop.Domain.Image
{
    public class Image : FullAuditedAggregateRoot<long>
    {
        public string Identified { get; set; }
        public string Url { get; set; }
        [ForeignKey(nameof(Image.Seller))]
        public long? SellerId { get; set; }
        public Seller.Seller Seller { get; set; }
    }
}
