﻿using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            var images = ImageRepository.GetAll().Where(s=>ids.Contains(s.Id));
            foreach (var image in images)
            {
                await ImageRepository.DeleteAsync(image);
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<IQueryable<Image>> GetImagesForSeller(long sellerId)
        {
            var images = ImageRepository.GetAll().Where(s=>s.SellerId == sellerId);
            return images;
        }

        public async Task<IQueryable<Image>> GetSystemImages()
        {
            var images = ImageRepository.GetAll().Where(s => s.SellerId == null);
            return images;
        }

        public async Task<long[]> UploadImages(long? sellerId, List<IFormFile> files)
        {
            var ids = new List<long>();
            foreach (var image in files)
            {
                if (!image.IsImage())
                {
                    throw new UserFriendlyException("Not valid image");
                }

                var timeStamp = DateTime.UtcNow.Ticks.ToString();
                var identified = $"{timeStamp}{Path.GetExtension(image.FileName)}";
                var storePath = Configuration.GetValue<string>("Files:ImageLocation");
                var urlPath = Path.Combine( storePath, identified);
                var filePath = Path.Combine("wwwroot", urlPath);

                if (image.Length > 0)
                {

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await image.CopyToAsync(stream);
                    }
                }

                var Image = new Image
                {
                    Url = urlPath,
                    SellerId = sellerId,
                    Identified = image.FileName
                };
                var id = await ImageRepository.InsertAndGetIdAsync(Image);
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

            var timeStamp = DateTime.UtcNow.Ticks.ToString();
            var identified = $"{timeStamp}{Path.GetExtension(image.FileName)}";
            var storePath = Configuration.GetValue<string>("Files:ImageLocation");
            var urlPath = Path.Combine(storePath, identified);
            var filePath = Path.Combine("wwwroot", urlPath);

            if (image.Length > 0)
            {

                using (var stream = System.IO.File.Create(filePath))
                {
                    await image.CopyToAsync(stream);
                }
            }

            var Image = new Image
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
