using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.ProductAdmin.Dto
{
    public class ProductEditDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long CoverImageId { get; set; }
        [Required]
        public long[] ImageIds { get; set; }
        public long[] CategoryIds { get; set; }
        [Required]
        public long BrandId { get; set; }
    }
}
