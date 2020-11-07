using Abp.Domain.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Image
{
    public interface IImageManager : IDomainService
    {
        public Task<long[]> UploadImages(long? sellerId, List<IFormFile> files);
        public Task<string> UploadWithResult(long? sellerId, IFormFile file);
        public Task DeleteImages(params long[] ids);
        public Task<IQueryable<Image>> GetImagesForSeller(long sellerId);
        public Task<IQueryable<Image>> GetSystemImages();
    }
}
