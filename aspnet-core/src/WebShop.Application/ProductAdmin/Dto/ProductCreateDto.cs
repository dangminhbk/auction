using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.ProductAdmin.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string Description { get; set; }
        public long? CoverImageId { get; set; }
        public ICollection<long> ImageIds { get; set; }
        public ICollection<long> CategoryIds { get; set; }
        public long SellerId { get; set; }
        public long? BrandId { get; set; }
    }
}
