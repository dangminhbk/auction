using Abp.Application.Services.Dto;

namespace WebShop.ProductAdmin.Dto
{
    public class ImageDto : EntityDto<long>
    {
        public string Url { get; set; }
    }
}
