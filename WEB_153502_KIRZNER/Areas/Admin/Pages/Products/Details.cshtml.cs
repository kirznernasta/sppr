using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Services.ProductService;

namespace WEB_153502_KIRZNER.Areas.Admin.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetProductByIdAsync((int)id);

            if (!response.Success)
            {
                return NotFound();
            }

            Product = response.Data;
            return Page();
        }
    }
}
