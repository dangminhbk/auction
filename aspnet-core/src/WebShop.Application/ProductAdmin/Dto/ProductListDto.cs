using System;

namespace WebShop.ProductAdmin.Dto
{
    public class ProductListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public long Sold { get; set; }
    }
}
