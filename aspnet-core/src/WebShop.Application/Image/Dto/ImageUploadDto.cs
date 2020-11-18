using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebShop.Domain.Image.Dto
{
    public class ImageUploadDto
    {
        public List<IFormFile> Files { get; set; }
    }
}
