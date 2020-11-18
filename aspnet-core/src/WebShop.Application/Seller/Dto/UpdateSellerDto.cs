using System.ComponentModel.DataAnnotations;

namespace WebShop.Seller.Dto
{
    public class UpdateSellerDto
    {
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string SellerName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public long? SellerLogo { get; set; }
        public long? SellerCover { get; set; }
    }
}
