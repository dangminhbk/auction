using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Seller.Dto
{
    public class PublicSellerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }

    public class PublicDetailSellerDto : PublicSellerDto
    {
        public string CoverUrl { get; set; }
        public string Description { get; set; }
    }
}
