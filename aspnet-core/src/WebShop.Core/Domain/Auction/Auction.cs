using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Auction
{
    public class Auction : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Auction.Seller))]
        public long SellerId { get; set; }
        public Seller.Seller Seller { get; set; }
        [ForeignKey(nameof(Auction.Product))]
        public long ProductId { get; set; }
        public Product.Product Product { get; set; }
        public decimal InitPrice { get; set; }
        public decimal MinAcceptPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CurrentPrice { get; set; }
        [ForeignKey(nameof(Auction.Winner))]
        public long? WinnerId { get; set; }
        public virtual User Winner { get; set; }
        public DateTime LastBidTime { get; set; }
        public long NumberOfBid { get; set; }
        public bool HasMakeInvoice { get; set; } = false;
    }
}
