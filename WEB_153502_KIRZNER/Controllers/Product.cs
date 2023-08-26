using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Services.CategoryService;
using WEB_153502_KIRZNER.Services.ProductService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_153502_KIRZNER.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            int? categoryId;
            if (category == null)
            {
                categoryId = null;
                ViewData["currentCategory"] = "Все";
            } else
            {
                var idResponse = await _categoryService.GetCategoryByNormalizedNameAsync(category);
                categoryId = idResponse.Data.Id;
                ViewData["currentCategory"] = idResponse.Data.Name;
            }

            var productResponse = await _productService.GetProductListAsync(categoryId, pageNo);
            var categoryResponse = await _categoryService.GetCategoryListAsync();
            if (categoryResponse.Success)
            {
                ViewData["categories"] = categoryResponse.Data;
            }

            if (!productResponse.Success)
            {
                return NotFound(productResponse.ErrorMessage);
            }
              
            return View(productResponse.Data);
        }
    }
}

