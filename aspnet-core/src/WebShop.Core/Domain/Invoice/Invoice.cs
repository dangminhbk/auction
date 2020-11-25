using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Invoice
{
    public class Invoice : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Invoice.Bid))]
        public long BidId { get; set; }
        public Bid.Bid Bid { get; set; }
        public OrderStatus PaymentStatus { get; set; }
        [ForeignKey(nameof(Invoice.User))]
        public long UserId { get; set; }
        public User User { get; set; }
        public string ProductName { get; set; }
        public string ReceiperName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey(nameof(Invoice.Seller))]
        public long SellerId { get; set; }
        public Seller.Seller Seller { get; set; }
        [ForeignKey(nameof(Invoice.Auction))]
        public long AuctionId { get; set; }
        public Auction.Auction Auction { get; set; }
        public string SerialNumber { get; set; }
    }

    public enum OrderStatus
    {
        Initial,
        Pending,
        Success,
        Cancel
    }
}
