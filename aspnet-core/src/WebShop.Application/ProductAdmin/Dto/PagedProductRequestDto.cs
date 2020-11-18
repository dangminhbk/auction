using Abp.Application.Services.Dto;

namespace WebShop.ProductAdmin.Dto
{
    public class PagedProductRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
