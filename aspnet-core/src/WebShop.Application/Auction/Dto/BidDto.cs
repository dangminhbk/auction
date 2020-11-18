using Abp.Application.Services.Dto;
using System;

namespace WebShop.Auction.Dto
{
    public class BidDto : EntityDto<long>
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTime { get; set; }
        public long AuctionId { get; set; }
    }
}
