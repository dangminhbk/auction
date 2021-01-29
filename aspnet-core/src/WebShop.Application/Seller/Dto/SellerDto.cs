using WebShop.Domain.Seller;

namespace WebShop.Seller.Dto
{
    public class SellerDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SellerLogoUrl { get; set; }
    }

    public class SellerDetailDto : SellerDto
    {
        public long? SellerLogoId { get; set; }
        public long? SellerCoverId { get; set; }
        public string SellerCoverUrl { get; set; }
        public PaymentRegisterStatus PaymentRegisterStatus { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public long Credit { get; set; }
    }
}
