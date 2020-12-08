using WebShop.DtoBase;

namespace WebShop.Auction.Dto
{
    public class PagedAuctionSearchDto : PagedSearchDto
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public long BrandId { get; set; }
    }
}
