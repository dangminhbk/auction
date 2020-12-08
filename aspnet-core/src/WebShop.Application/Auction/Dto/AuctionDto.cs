using Abp.Application.Services.Dto;
using System;

namespace WebShop.Auction.Dto
{
    public class AuctionDto : EntityDto<long>
    {
        public long ProductId { get; set; }
        public decimal InitPrice { get; set; }
        public decimal MinAcceptPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal CurrentPrice { get; set; }
        public long NumberOfBids { get; set; }
        public string UserName { get; set; }
        public DateTime LastBidTime { get; set; }
        public long SellerId { get; set; }
    }
}
