using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;
using WEB_153502_KIRZNER.Extensions;
using WEB_153502_KIRZNER.Services.CategoryService;
using WEB_153502_KIRZNER.Services.ProductService;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var categoryResponse = await _categoryService.GetCategoryListAsync();
            if (categoryResponse.Success)
            {
                ViewData["categories"] = categoryResponse.Data;
            }
            if (category == null)
            {
                ViewData["currentCategory"] = "Все";
            }
            else
            {
                if (categoryResponse.Success)
                {
                    ViewData["currentCategory"] = categoryResponse.Data.First((c) => c.NormalizedName.Equals(category)).Name;
                }
                else
                {
                    return NotFound("Ошибка: нет такой категории");
                }
            }

            var productResponse = await _productService.GetProductListAsync(category, pageNo);
            

            if (!productResponse.Success)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            var data = productResponse.Data;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", data);
            }
            return View(data);
        }
    }
}

