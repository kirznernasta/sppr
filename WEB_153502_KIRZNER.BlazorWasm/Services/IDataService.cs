﻿using System;
using WEB_153502_KIRZNER.Domain.Entities;

namespace WEB_153502_KIRZNER.BlazorWasm.Services
{
	public interface IDataService
	{
        event Action DataLoaded;

        List<Category> Categories { get; set; }

        List<Product> ObjectsList { get; set; }

        bool Success { get; set; }

        string ErrorMessage { get; set; }

        int TotalPages { get; set; }

        int CurrentPage { get; set; }

        public Task GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);

        public Task<Product> GetProductByIdAsync(int id);
        
        public Task GetCategoryListAsync();
    }
}

