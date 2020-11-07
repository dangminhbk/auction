using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.ProductAdmin.Dto
{
    public class PagedProductRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
