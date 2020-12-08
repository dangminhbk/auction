using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Category.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryDto : EntityDto<long>
    {
        [Required]
        public string Name { get; set; }
    }
}
