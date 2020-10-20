﻿using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Product;
using WebShop.ProductAdmin.Dto;

namespace WebShop.ProductAdmin
{
    public class ProductAdminAppService : WebShopAppServiceBase
    {
        private readonly IProductManager ProductManager;
        private readonly IRepository<Domain.Seller.Seller, long> SellerRepository;
        public ProductAdminAppService(IProductManager productManager, IRepository<Domain.Seller.Seller, long> sellerRepository)
        {
            ProductManager = productManager;
            SellerRepository = sellerRepository;
        }
        public async Task Create(ProductCreateDto productCreateDto)
        {
            // Get seller info
            var userId =( await GetCurrentUserAsync()).Id;
            var seller = await SellerRepository.
                                FirstOrDefaultAsync(s => s.UserId == userId);
            // Check images asset

            // 
        }
    }
}