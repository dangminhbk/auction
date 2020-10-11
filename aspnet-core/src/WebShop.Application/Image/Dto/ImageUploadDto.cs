using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Image.Dto
{
    public class ImageUploadDto
    {
        public List<IFormFile> Files { get; set; }
    }
}
