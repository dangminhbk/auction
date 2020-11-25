using Abp.Application.Services.Dto;
using System;

namespace WebShop.Auction.Dto
{
    public class AuctionListDto : EntityDto<long>
    {
        public DateTime StartTime { get; set; }
        public string ProductName { get; set; }
        public DateTime EndTime { get; set; }
        public string ProductImage { get; set; }
        public decimal CurrentPrice { get; set; }
        public long NumberOfBid { get; set; }
        public bool IsActive { get; set; }
    }
}
