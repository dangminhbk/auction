using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;

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
