using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Brand.Dto
{
    public class PagedBrandRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
