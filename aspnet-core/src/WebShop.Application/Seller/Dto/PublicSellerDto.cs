namespace WebShop.Seller.Dto
{
    public class PublicSellerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }

    public class PublicDetailSellerDto : PublicSellerDto
    {
        public string CoverUrl { get; set; }
        public string Description { get; set; }

        public long OrderCount { get; set; }
        public long AuctionCount { get; set; }
    }
}
