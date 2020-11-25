using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.DtoBase
{
    public class PagedSearchDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
