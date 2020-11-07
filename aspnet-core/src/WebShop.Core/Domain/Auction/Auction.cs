using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Auction
{
    public class Auction : FullAuditedAggregateRoot<long>
    {
        public long SellerId { get; set; }
        public Seller.Seller Seller { get; set; }
        public long ProductId { get; set; }
        public Product.Product Product { get; set; }
        public decimal InitPrice { get; set; }
        public decimal MinAcceptPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
