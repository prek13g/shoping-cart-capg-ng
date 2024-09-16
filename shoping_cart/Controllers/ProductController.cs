using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTOs;
using Shoping_cart.Models;

namespace Shoping_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DBContext _context;

        public ProductsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        // [Authorize(Policy = "UserOnly")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Select(p => new ProductDTO
                {
                    Product_id = p.Product_id,
                    Product_name = p.Product_name,
                    Product_description = p.Product_description,
                    Product_Quantity = p.Product_Quantity,
                    Product_Price = p.Product_Price,
                    Category_id = p.Category_id,
                    image = p.image
                })
                .ToListAsync();

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _context.Products
                .Where(p => p.Product_id == id)
                .Select(p => new ProductDTO
                {
                    Product_id = p.Product_id,
                    Product_name = p.Product_name,
                    Product_description = p.Product_description,
                    Product_Quantity = p.Product_Quantity,
                    Product_Price = p.Product_Price,
                    Category_id = p.Category_id,
                    image = p.image
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        // GET: api/Products/5
        [HttpGet("search /{category_name}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(string category_name)
        {
            var products = await _context.Products
                .Where(p => p.Category.Category_name == category_name)
                .Select(p => new ProductDTO
                {
                    Product_id = p.Product_id,
                    Product_name = p.Product_name,
                    Product_description = p.Product_description,
                    Product_Quantity = p.Product_Quantity,
                    Product_Price = p.Product_Price,
                    Category_id = p.Category_id,
                    image = p.image
                })
                .ToListAsync();

            if (!products.Any())
            {
                return NotFound("No products available for the given category.");
            }

            return Ok(products);
        }

//method for product search
        [HttpGet("search/{query}")]
public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProducts(string query)
{
    var products = await _context.Products
        .Where(p => p.Product_name.Contains(query) || p.Category.Category_name.Contains(query))
        .Select(p => new ProductDTO
        {
            Product_id = p.Product_id,
            Product_name = p.Product_name,
            Product_description = p.Product_description,
            Product_Quantity = p.Product_Quantity,
            Product_Price = p.Product_Price,
            Category_id = p.Category_id,
            image = p.image
        })
        .ToListAsync();

    if (!products.Any())
    {
        return NotFound("No products found.");
    }

    return Ok(products);
}




        // PUT: api/Products/5
        [HttpPut("{id}")]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Product_id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Map ProductDTO to Product
            product.Product_name = productDto.Product_name;
            product.Product_description = productDto.Product_description;
            product.Product_Quantity = productDto.Product_Quantity;
            product.Product_Price = productDto.Product_Price;
            product.Category_id = productDto.Category_id;
            product.image = productDto.image;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Product updated successfully.");
        }

        // POST: api/Products
        [HttpPost]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is null");
            }

            var product = new Product
            {
                Product_name = productDto.Product_name,
                Product_description = productDto.Product_description,
                Product_Quantity = productDto.Product_Quantity,
                Product_Price = productDto.Product_Price,
                Category_id = productDto.Category_id,
                image = productDto.image
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var createdProductDto = new ProductDTO
            {
                Product_id = product.Product_id,
                Product_name = product.Product_name,
                Product_description = product.Product_description,
                Product_Quantity = product.Product_Quantity,
                Product_Price = product.Product_Price,
                Category_id = product.Category_id,
                image = productDto.image
            };

            // Return a custom message with the created product details
            return StatusCode(StatusCodes.Status201Created, new { message = "Product successfully created.", product = createdProductDto });
        }

         [HttpGet("api/products")]
public IActionResult GetProducts([FromQuery] string q)
{
    // Assuming you have a list of products stored in a variable or fetched from a database
    var products = _context.Products.ToList(); // Replace with your actual data fetching logic

    if (!string.IsNullOrEmpty(q))
    {
        var filteredProducts = products.Where(p => p.Product_name.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(filteredProducts);
    }

    return Ok(products);
}

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Product deleted");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_id == id);
        }
    }

   

}

