using System;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.Services.ProductService
{
	public interface IProductService
	{
        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(int? categoryId, int pageNo = 1);
        public Task<ResponseData<Product>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile);
    }
}

