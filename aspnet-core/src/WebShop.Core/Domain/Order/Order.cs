using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Order
{
    public class Order : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Order.Bid))]
        public long BidId { get; set; }
        public Bid.Bid Bid { get; set; }
        public string Address { get; set; }
        public OrderStatus PaymentStatus { get; set; }
        [ForeignKey(nameof(Order.User))]
        public long UserId { get; set; }
        public User User { get; set; }
    }

    public enum OrderStatus 
    { 
        Pending,
        Paid,
        Success,
        Cancel
    }
}
