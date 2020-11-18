using Abp.Application.Services.Dto;

namespace WebShop.Brand.Dto
{
    public class PagedBrandRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
