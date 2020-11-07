using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Order
{
    public class Order : FullAuditedAggregateRoot<long>
    {
        public long BidId { get; set; }
        public Bid.Bid Bid { get; set; }
        public string Address { get; set; }
        public OrderStatus PaymentStatus { get; set; }
    }

    public enum OrderStatus 
    { 
        Pending,
        Paid,
        Success,
        Cancel
    }
}
