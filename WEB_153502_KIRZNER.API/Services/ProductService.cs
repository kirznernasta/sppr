using System;
using Microsoft.EntityFrameworkCore;
using WEB_153502_KIRZNER.API.Data;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.API.Services
{
	public class ProductService : IProductService
	{
        private AppDbContext _context;
        private readonly int _maxPageSize = 20;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public Task<ResponseData<Product>> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }
                
            var query = _context.Products.ToList();
            var dataList = new ProductListModel<Product>();

            query = query.Where(product => categoryNormalizedName == null || product.CategoryNormalizedName.Equals(categoryNormalizedName)).ToList();

            var count = query.Count();
            if (count == 0)
            {
                var result = new ResponseData<ProductListModel<Product>>
                {
                    Data = dataList
                };
                return Task.FromResult(result);
            }

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
            {
                var result = new ResponseData<ProductListModel<Product>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No such page"
                };
                return Task.FromResult(result);
            }
                
            dataList.Items = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            var response = new ResponseData<ProductListModel<Product>>
            {
                Data = dataList
            };
            return Task.FromResult(response);
        }

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}

