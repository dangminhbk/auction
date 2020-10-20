using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.Seller.Dto
{
    public class UpdateSellerDto
    {
        public long SellerId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(100)]
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
