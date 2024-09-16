using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTOs;
using Shoping_cart.Models;

namespace Shoping_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policy ="UserOnly")]
    public class CartsController : ControllerBase
    {
        private readonly DBContext _context;

        public CartsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetCarts()
        {
            var carts = await _context.Carts
                .Select(c => new CartDTO
                {
                    Cart_id = c.Cart_id,
                    User_id = c.User_id,
                    Product_id = c.Product_id,
                    Quantity = c.Quantity
                })
                .ToListAsync();

            return Ok(carts);
        }

        // GET: api/Carts/5
        //[HttpGet("{id}")]
        // GET: api/Carts/5
        [HttpGet("{userid}")]
        public async Task<ActionResult<CartDTO>> GetCart(int userid)
        {
            // Fetch the cart based on the ID
            var cart = await _context.Carts
                .Where(c => c.User_id == userid)
                .Select(c => new CartDTO
                {
                    Cart_id = c.Cart_id,
                    User_id = c.User_id,
                    Product_id = c.Product_id,
                    Quantity = c.Quantity
                })
                .ToListAsync();

            // Check if the cart exists
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Verify that the cart belongs to the currently logged-in user
            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || int.Parse(userId) != cart.User_id)
            {
                return Forbid(); // Forbidden if the user does not own the cart
            }*/

            return Ok(cart);
        }
        /* [HttpGet]
         [Authorize(Policy = "UserOnly")]
         public async Task<ActionResult<IEnumerable<CartDTO>>> GetCart()
         {
             // Retrieve the current user's ID from the claims
             var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

             if (userIdString == null || !int.TryParse(userIdString, out var userId))
             {
                 return Unauthorized("User ID is not available in the context.");
             }

             // Fetch the cart items for the current user
             var cartItems = await _context.Carts
                 .Where(c => c.User_id == userId)
                 .Select(c => new CartDTO
                 {
                     Cart_id = c.Cart_id,
                     User_id = c.User_id,
                     Product_id = c.Product_id,
                     Quantity = c.Quantity
                 })
                 .ToListAsync();

             // Check if the cart has any items
             if (cartItems == null || !cartItems.Any())
             {
                 return NotFound("No items found in the cart.");
             }

             return Ok(cartItems);
         }



         /*
         public async Task<ActionResult<CartDTO>> GetCart(int id)
         {
             var cart = await _context.Carts
                 .Where(c => c.Cart_id == id)
                 .Select(c => new CartDTO
                 {
                     Cart_id = c.Cart_id,
                     User_id = c.User_id,
                     Product_id = c.Product_id,
                     Quantity = c.Quantity
                 })
                 .FirstOrDefaultAsync();

             if (cart == null)
             {
                 return NotFound();
             }

             return Ok(cart);
         }*/
        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, [FromBody] CartDTO cartDto)
        {
            if (id != cartDto.Cart_id)
            {
                return BadRequest();
            }

            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }
            // Verify that the cart belongs to the currently logged-in user
            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != cart.User_id.ToString())
            {
                return Forbid(); // Forbidden if the user does not own the cart
            }*/

            // Update Cart properties
            //cart.User_id = cartDto.User_id;
            //cart.User_id = int.Parse(userId);
            cart.User_id = cartDto.User_id;
            cart.Product_id = cartDto.Product_id;
            cart.Quantity = cartDto.Quantity;

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        [HttpPost]
        public async Task<ActionResult<CartDTO>> PostCart([FromBody] CartDTO cartDto)
        {
            if (cartDto == null)
            {
                return BadRequest("Cart data is null");
            }
            // Get the user ID from the claims principal
            /*var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
 
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }*/
            var cart = new Cart
            {
                User_id = cartDto.User_id,
                Product_id = cartDto.Product_id,
                Quantity = cartDto.Quantity
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            var createdCartDto = new CartDTO
            {
                Cart_id = cart.Cart_id,
                User_id = cart.User_id,
                Product_id = cart.Product_id,
                Quantity = cart.Quantity
            };

            return CreatedAtAction(nameof(GetCart), new { userid = cart.User_id }, createdCartDto);
        }
        

        [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] CartDTO cartDto)
    {
        var cartItem = new Cart
        {
             User_id = cartDto.User_id,
                Product_id = cartDto.Product_id,
                Quantity = cartDto.Quantity
        };

        _context.Carts.Add(cartItem);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Product added to cart successfully" });
    }



        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Cart_id == id);
        }
    }
}

