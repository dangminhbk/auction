using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Product;

namespace WebShop.Domain.Image
{
    public class ImageManager : DomainService, IImageManager
    {
        private readonly IRepository<Image, long> ImageRepository;
        private readonly IConfiguration Configuration;
        public ImageManager(IConfiguration configuration, IRepository<Image, long> imageRepository)
        {
            Configuration = configuration;
            ImageRepository = imageRepository;
        }
        public async Task DeleteImages(params long[] ids)
        {
            IQueryable<Image> images = ImageRepository.GetAll().Where(s => ids.Contains(s.Id));
            foreach (Image image in images)
            {
                await ImageRepository.DeleteAsync(image);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<IQueryable<Image>> GetImagesForSeller(long sellerId)
        {
            IQueryable<Image> images = ImageRepository.GetAll().Where(s => s.SellerId == sellerId);
            return images;
        }

        public async Task<IQueryable<Image>> GetSystemImages()
        {
            IQueryable<Image> images = ImageRepository.GetAll().Where(s => s.SellerId == null);
            return images;
        }

        public async Task<long[]> UploadImages(long? sellerId, List<IFormFile> files)
        {
            List<long> ids = new List<long>();
            foreach (IFormFile image in files)
            {
                if (!image.IsImage())
                {
                    throw new UserFriendlyException("Not valid image");
                }

                string timeStamp = DateTime.UtcNow.Ticks.ToString();
                string identified = $"{timeStamp}{Path.GetExtension(image.FileName)}";
                string storePath = Configuration.GetValue<string>("Files:ImageLocation");
                string urlPath = Path.Combine(storePath, identified);
                string filePath = Path.Combine("wwwroot", urlPath);

                if (image.Length > 0)
                {

                    using (FileStream stream = System.IO.File.Create(filePath))
                    {
                        await image.CopyToAsync(stream);
                    }
                }

                Image Image = new Image
                {
                    Url = urlPath,
                    SellerId = sellerId,
                    Identified = image.FileName
                };
                long id = await ImageRepository.InsertAndGetIdAsync(Image);
                ids.Add(id);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return ids.ToArray();
        }

        public async Task<string> UploadWithResult(long? sellerId, IFormFile image)
        {
            if (!image.IsImage())
            {
                throw new UserFriendlyException("Not valid image");
            }

            string timeStamp = DateTime.UtcNow.Ticks.ToString();
            string identified = $"{timeStamp}{Path.GetExtension(image.FileName)}";
            string storePath = Configuration.GetValue<string>("Files:ImageLocation");
            string urlPath = Path.Combine(storePath, identified);
            string filePath = Path.Combine("wwwroot", urlPath);

            if (image.Length > 0)
            {

                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    await image.CopyToAsync(stream);
                }
            }

            Image Image = new Image
            {
                Url = urlPath,
                SellerId = sellerId,
                Identified = image.FileName
            };
            await ImageRepository.InsertAsync(Image);
            return Image.Url;
        }
    }
}
