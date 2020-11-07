using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.ProductAdmin.Dto
{
    public class ProductSellerDetail
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public long? CoverImageId { get; set; }
        public string CoverImageUrl { get; set; }
        public long[] ImageIds { get; set; }
        public string[] ImageUrls { get; set; }
        public long[] CategoryIds { get; set; }
        public long SellerId { get; set; }
        public long? BrandId { get; set; }
    }
}
