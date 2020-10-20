using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.Brand.Dto
{
    public class CreateBrandDto
    {
        [Required]
        public string Name { get; set; }
        public long? BrandImageId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
