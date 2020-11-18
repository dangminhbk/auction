using Abp.Application.Services.Dto;

namespace WebShop.Seller.Dto
{
    public class PagedSellerRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
