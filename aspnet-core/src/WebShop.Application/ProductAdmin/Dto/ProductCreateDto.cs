using System.ComponentModel.DataAnnotations;

namespace WebShop.ProductAdmin.Dto
{
    public class ProductCreateDto
    {
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
