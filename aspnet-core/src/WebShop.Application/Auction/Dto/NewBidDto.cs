using System;

namespace WebShop.Auction.Dto
{
    public class NewBidDto
    {
        public decimal Price { get; set; }
        public string Username { get; set; }
        public DateTime BidTime { get; set; }
        public long AuctionId { get; set; }
    }
}
