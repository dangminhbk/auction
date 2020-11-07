using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Bid
{
    public class Bid : FullAuditedAggregateRoot<long>
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTime { get; set; }
        public long AuctionId { get; set; }
        public Auction.Auction Auction { get; set; }
    }
}
