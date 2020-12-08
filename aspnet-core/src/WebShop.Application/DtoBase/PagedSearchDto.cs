using Abp.Application.Services.Dto;

namespace WebShop.DtoBase
{
    public class PagedSearchDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long[] CategoryIds { get; set; }
        public long? SellerId { get; set; }
        public long? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
