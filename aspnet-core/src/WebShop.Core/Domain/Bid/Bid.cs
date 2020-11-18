using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Bid
{
    public class Bid : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Bid.User))]
        public long UserId { get; set; }
        public User User { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTime { get; set; }
        [ForeignKey(nameof(Bid.Auction))]
        public long AuctionId { get; set; }
        public Auction.Auction Auction { get; set; }
    }
}
