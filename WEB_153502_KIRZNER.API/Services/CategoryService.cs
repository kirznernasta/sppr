using System;
using Microsoft.EntityFrameworkCore;
using WEB_153502_KIRZNER.API.Data;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.API.Services
{
	public class CategoryService : ICategoryService
	{
        private AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ResponseData<List<Category>> { Data = categories };
        }
    }
}

