using System.ComponentModel.DataAnnotations;

namespace WebShop.Authorization.Accounts.Dto
{
    public class RegisterSellerInput : RegisterInput
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
    }
}
