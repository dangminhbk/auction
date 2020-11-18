using System.ComponentModel.DataAnnotations;

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
