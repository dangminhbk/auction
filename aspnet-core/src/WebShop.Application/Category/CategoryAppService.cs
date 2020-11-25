using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Category.Dto;

namespace WebShop.Category
{
    public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, long>
    {
        public CategoryAppService(IRepository<Category, long> repository) : base(repository)
        {
        }
        public async Task<List<CategoryDto>> GetDropdown()
        {
            var result = await this.Repository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<CategoryDto>>(result);
        }
    }
}
