using System;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;
using WEB_153502_KIRZNER.Services.CategoryService;

namespace WEB_153502_KIRZNER.Services.ProductService
{
    public class MemoryProductService : IProductService
	{
        private List<Product> _products = new();
        private List<Category> _categories = new();
        private int _itemsPerPage;

        public MemoryProductService([FromServices] IConfiguration config, ICategoryService categoryService)
		{
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            _itemsPerPage = config.GetValue<int>("ItemsPerPage");
            SetUpData();
		}

        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
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

        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(int? categoryId, int pageNo = 1)
        {
            var products = _products;
            if (categoryId != null)
            {
                products = _products.Where((product) => product.CategoryId.Equals(categoryId)).ToList();
            }

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);

            var selectedProducts = products
                .Skip((pageNo - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            var result = new ResponseData<ProductListModel<Product>> { Data = new ProductListModel<Product> { Items = selectedProducts, CurrentPage = pageNo, TotalPages = totalPages } };
            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetUpData()
        {
            _products = new List<Product>
            {
                new Product{Id = 1, Name = "Мячик Теннисный", Description = "Способствует развитию физической активности и челюстной мускулатуры", Price = 7.10, Image = "Images/ТеннисныйМяч.jpeg", CategoryId = 1},
                new Product{Id = 2, Name = "Плюшевый Мишка", Description = "Мягкая игрушка для собак в виде забавного медвежонка.", Price = 10.0, Image = "Images/ПлюшевыйМишка.jpeg", CategoryId = 1},
                new Product{Id = 3, Name = "Кормовая добавка", Description = "Предназначена для нормализации обмена веществ и поддержания иммунитета у собак", Price = 9.0, Image = "Images/ТаблеткиКруглые.jpeg", CategoryId = 2},
                new Product{Id = 4, Name = "Пребиотик", Description = "Восстанавливает полезную микрофлору кишечника и укрепляет иммунитет", Price = 15.60, Image = "Images/ТаблеткиОвальные.jpeg", CategoryId = 2},
                new Product{Id = 5, Name = "Плащ жёлтый", Description = "Удобный и функциональный аксессуар для собак, который просто незаменим в дождливую погоду", Price = 33.70, Image = "Images/ПлащЖёлтый.jpeg", CategoryId = 3},
                new Product{Id = 6, Name = "Свитер розовый", Description = "Тёплый, удобный и функциональный аксессуар для собак, который просто незаменим в холодную погоду", Price = 28.70, Image = "Images/СвитерВязаныйРозовый.jpeg", CategoryId = 3},
                new Product{Id = 7, Name = "Косточка из оленины", Description = "Косточка для чистки зубов из оленины", Price = 6.60, Image = "Images/Косточка.jpeg", CategoryId = 4},
                new Product{Id = 8, Name = "Сухой корм для взрослых собак всех пород", Description = "Cодержит все белки, необходимые для развития мышц и тканей", Price = 185.20, Image = "Images/КормРазноцветный.jpeg", CategoryId = 4},
                new Product{Id = 9, Name = "Ошейник голубой с подвеской", Description = "Надежный помощник в выгуле собак, регулируемый", Price = 12.20, Image = "Images/ОшейникГолубойСПодвеской.jpeg", CategoryId = 5},
                new Product{Id = 10, Name = "Ошейник зелёный с подвеской", Description = "Надежный помощник в выгуле собак, регулируемый", Price = 12.20, Image = "Images/ОшейникЗелёныйСПодвеской.jpeg", CategoryId = 5},
            };
        }
    }
}

