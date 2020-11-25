using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.ProductAdmin.Dto
{
    public class ImageDto : EntityDto<long>
    {
        public string Url { get; set; }
    }
}
