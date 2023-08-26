using System;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.Services.CategoryService
{
	public interface ICategoryService
	{
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
        public Task<ResponseData<Category>> GetCategoryByNormalizedNameAsync(string normalizedName);
    }
}

