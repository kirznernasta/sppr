using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153502_KIRZNER.API.Data;
using WEB_153502_KIRZNER.API.Services;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;

namespace WEB_153502_KIRZNER.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: api/Products
        [HttpGet]
        [HttpGet("{category}")]
        [HttpGet("page{pageNo:int}")]
        [HttpGet("size{pageSize:int}")]
        [HttpGet("{category}/page{pageNo:int}")]
        [HttpGet("{category}/size{pageSize:int}")]
        [HttpGet("page{pageNo:int}/size{pageSize:int}")]
        [HttpGet("{category}/page{pageNo:int}/size{pageSize:int}")]
        public async Task<ActionResult<ResponseData<List<Product>>>> GetProducts(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _productService.GetProductListAsync(category, pageNo, pageSize));
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            throw new NotImplementedException();
            //if (_context.Products == null)
            //{
            //    return NotFound();
            //}
            //  var product = await _context.Products.FindAsync(id);

            //  if (product == null)
            //  {
            //      return NotFound();
            //  }

            //  return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            throw new NotImplementedException();
            //if (id != product.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(product).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProductExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            throw new NotImplementedException();
            //  if (_context.Products == null)
            //  {
            //      return Problem("Entity set 'AppDbContext.Products'  is null.");
            //  }
            //    _context.Products.Add(product);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

            // DELETE: api/Products/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //if (_context.Products == null)
            //{
            //    return NotFound();
            //}
            //var product = await _context.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();

            //return NoContent();
            throw new NotImplementedException();
        }

        private bool ProductExists(int id)
        {
            //return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
             throw new NotImplementedException();
        }
    }
}
