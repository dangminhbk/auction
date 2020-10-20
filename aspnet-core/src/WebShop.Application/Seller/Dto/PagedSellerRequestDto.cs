using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Seller.Dto
{
    public class PagedSellerRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
