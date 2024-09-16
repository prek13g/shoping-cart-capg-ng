using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Shoping_cart.DTOs;
using Shoping_cart.DatabaseContext;
using Shoping_cart.Services;
using Shoping_cart.Models;

namespace Shoping_cart.Controllers

{


    [Route("api/[controller]")]
    [ApiController]
    public class paymentController : ControllerBase
    {


        private readonly DBContext _context;
        private readonly CartService _cartService;


        // private readonly CartsController _cartController;

        public paymentController(DBContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;


        }


        [HttpPost("{userId}/add-product/{productId}")]
        public async Task<ActionResult> AddProductToOrder(int userId, int productId, [FromBody] int productQuantity)
        {
            // Validate if the user exists
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Validate if the product exists
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Always create a new order entry for the same product
            var newOrder = new Order
            {
                User_id = userId,
                Product_id = productId,
                Product_Quantity = productQuantity,
                Product_Price = product.Product_Price,
                Product_TotalAmount = productQuantity * product.Product_Price,
                OrderDateTime = DateTime.Now
            };

            // Add the new order to the database
            _context.Orders.Add(newOrder);

            // Save changes to the database
            await _context.SaveChangesAsync();

            //return Ok("Product added to order successfully.");
            return Ok(new { message = "Product added to order successfully." });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> payment(int id)
        {
            var cart = await _context.Carts
            .Include(c => c.Product) // Include the Product navigation property
            .Where(c => c.Cart_id == id)
            .FirstOrDefaultAsync();

            if (cart == null)
            {
                return NotFound("Cart not found"); // Return a 404 if no cart is found
            }
            // Check if Product is loaded
            if (cart.Product == null)
            {
                return StatusCode(500, "Product information is missing");
            }

            decimal b = cart.Product.Product_Price * cart.Quantity;
            var currentDateTime = DateTime.UtcNow; // Prefer UTC for consistency
            var order = new Order
            {
                User_id = cart.User_id,
                Product_id = cart.Product_id,
                OrderDateTime = currentDateTime,
                Product_Quantity = cart.Quantity,
                Product_Price = cart.Product.Product_Price,
                Product_TotalAmount = b,


            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            try
            {
                await _cartService.RemoveProductFromCartAsync(order.User_id, order.Product_id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok("Payment successful");

        }


    }
}

