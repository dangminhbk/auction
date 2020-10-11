using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Image.Dto
{
    [AutoMap(typeof(Image))]
    public class ImageListDto : Entity<long>
    {
        public string Identified { get; set; }
        public string Url { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}
