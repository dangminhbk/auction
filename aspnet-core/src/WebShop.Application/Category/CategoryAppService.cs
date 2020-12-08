using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            List<Category> result = await Repository.GetAll().ToListAsync();
            return ObjectMapper.Map<List<CategoryDto>>(result);
        }
    }
}
