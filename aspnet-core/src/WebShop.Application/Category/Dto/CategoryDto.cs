using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebShop.Category.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryDto : EntityDto<long>
    {
        [Required]
        public string Name { get; set; }
    }
}
