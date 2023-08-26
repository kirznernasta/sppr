using System;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.API.Services
{
	public interface ICategoryService
	{
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}

