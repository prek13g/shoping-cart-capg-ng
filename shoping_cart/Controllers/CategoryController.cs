using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTO;
using Shoping_cart.Models;
using System.Linq;
using System.Threading.Tasks;
using Shoping_cart.DTO;

namespace Shoping_cart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Policy = "AdminOnly")]
    public class CategoriesController : ControllerBase
    {
        private readonly DBContext _context;

        public CategoriesController(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is null");
            }

            var category = new Category
            {
                Category_name = categoryDto.Category_name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Return the created category DTO
            return Ok(new
            {
                Message = "Category successfully added."
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Category_id = c.Category_id,
                    Category_name = c.Category_name
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Category_id == id)
                .Select(c => new CategoryDto
                {
                    Category_id = c.Category_id,
                    Category_name = c.Category_name
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is null");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Category_name = categoryDto.Category_name;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return the updated category DTO
            return Ok(new CategoryDto
            {
                Category_id = category.Category_id,
                Category_name = category.Category_name
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok("Product deleted successfully.");
        }
    }
}

