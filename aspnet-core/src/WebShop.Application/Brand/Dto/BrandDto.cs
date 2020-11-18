using Abp.Domain.Entities;

namespace WebShop.Brand.Dto
{
    public class BrandDto : Entity<long>
    {
        public string Name { get; set; }
        public string BrandImageUrl { get; set; }
        public long? BrandImageId { get; set; }
        public string Description { get; set; }
    }
}
