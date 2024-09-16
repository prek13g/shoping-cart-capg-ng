using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTOs;
using Shoping_cart.Models;
using Shoping_cart.DatabaseContext;
using Shoping_cart.services;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Shoping_cart.Services;

namespace Shoping_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly CartService _cartService;


        // private readonly CartsController _cartController;

        public OrdersController(DBContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // GET: api/Orders
        [HttpGet]
       // [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Select(o => new OrderDTO
                {
                    Order_id = o.Order_id,
                    User_id = o.User_id,
                    Product_id = o.Product_id,
                    OrderDateTime = o.OrderDateTime,
                    Product_Quantity=o.Product_Quantity,
                    Product_Price=o.Product_Price,
                    Product_TotalAmount = o.Product_TotalAmount                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        //{
        //    var order = await _context.Orders
        //        .Where(o => o.Order_id == id)
        //        .Select(o => new OrderDTO
        //        {
        //            Order_id = o.Order_id,
        //            User_id = o.User_id,
        //            Product_id = o.Product_id,
        //            OrderDateTime = o.OrderDateTime,
        //            Product_TotalAmount = o.Product_TotalAmount

        //        })
        //        .FirstOrDefaultAsync();

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(order);
        //}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.User_id == userId)
                .Select(o => new OrderDTO
                {
                    Order_id = o.Order_id,
                    User_id = o.User_id,
                    Product_id = o.Product_id,
                    OrderDateTime = o.OrderDateTime,
                    Product_Quantity=o.Product_Quantity,
                    Product_Price=o.Product_Price,
                    Product_TotalAmount = o.Product_TotalAmount
                })
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for the specified user.");
            }

            return Ok(orders);
        }



        /*
                [HttpGet]
                [Authorize(Policy = "UserOnly")]
                public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUser()
                {
                    // Retrieve the user ID from the request's user context
                    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (userIdString == null || !int.TryParse(userIdString, out var userId))
                    {
                        return Unauthorized("User ID is not available in the context.");
                    }

                    // Fetch orders for the authenticated user
                    var orders = await _context.Orders
                        .Where(o => o.User_id == userId)
                        .Select(o => new OrderDTO
                        {
                            Order_id = o.Order_id,
                            User_id = o.User_id,
                            Product_id = o.Product_id,
                            OrderDateTime = o.OrderDateTime,
                            Product_TotalAmount = o.Product_TotalAmount
                        })
                        .ToListAsync();

                    if (!orders.Any())
                    {
                        return NotFound("No orders found for the specified user.");
                    }

                    return Ok(orders);
                }
               */

        // PUT: api/Orders/5
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutOrder(int id, [FromBody] OrderDTO orderDto)
         {
             if (id != orderDto.Order_id)
             {
                 return BadRequest();
             }
 
             var order = await _context.Orders.FindAsync(id);
 
             if (order == null)
             {
                 return NotFound();
             }
 
             // Update order properties
             order.User_id = orderDto.User_id;
             order.Product_id = orderDto.Product_id;
             order.OrderDateTime = orderDto.OrderDateTime;
             order.Product_TotalAmount = orderDto.Product_TotalAmount;
 
 
             _context.Entry(order).State = EntityState.Modified;
 
             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!OrderExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }
 
             return NoContent();
         }*/

        // POST: api/Orders
        /*  [HttpPost]
          public async Task<ActionResult<OrderDTO>> PostOrder([FromBody] OrderDTO orderDto)
          {
              if (orderDto == null)
              {
                  return BadRequest("Order data is null");
              }
 
              var order = new Order
              {
                  User_id = orderDto.User_id,
                  Product_id = orderDto.Product_id,
                  OrderDateTime = orderDto.OrderDateTime,
                  Product_TotalAmount = orderDto.Product_TotalAmount,
 
              };
 
              _context.Orders.Add(order);
              await _context.SaveChangesAsync();
              //_cartService.RemoveProductFromCartAsync(order.User_id, order.Product_id);
              // Await the asynchronous call to ensure the product is removed from the cart
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
        */


        // CartsController a = new CartsController(_context);
        // a.DeleteCart(orderDto.Cart_id);
        /* if (orderDto.Cart_id.HasValue)
         {
             await _cartController.DeleteCartAsync(orderDto.Cart_id.Value);
         }*/



        /*  var createdOrderDto = new OrderDTO
          {
              Order_id = order.Order_id,
              User_id = order.User_id,
              Product_id = order.Product_id,
              OrderDateTime = order.OrderDateTime,
              Product_TotalAmount = order.Product_TotalAmount,
 
          };
 
          return CreatedAtAction(nameof(GetOrdersByUser), new { id = order.Order_id }, createdOrderDto);
      }
        */
        // DELETE: api/Orders/5
        /* [HttpDelete("cancellation/{id}")]
 
         public async Task<IActionResult> DeleteOrder(int id)
         {
             var order = await _context.Orders.FindAsync(id);
             if (order == null)
             {
                 return NotFound();
             }
 
             _context.Orders.Remove(order);
             await _context.SaveChangesAsync();
 
             return NoContent();
         }*/

        [HttpPut("{id}/cancel")]
public async Task<IActionResult> CancelOrder(int id)
{
    var order = await _context.Orders.FindAsync(id);

    if (order == null)
    {
        return NotFound();
    }

    order.Status = "cancelled";
    _context.Entry(order).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!OrderExists(id))
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

// private bool OrderExists(int id)
// {
//     return _context.Orders.Any(e => e.Id == id);
// }


        [HttpDelete("cancellation/{id}")]
        //[Authorize(Policy = "UserOnly")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            // Retrieve the current user's ID from the claims
            // var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // if (userIdString == null || !int.TryParse(userIdString, out var userId))
            // {
            //     return Unauthorized("User ID is not available in the context.");
            // }

            // Fetch the order to be deleted and ensure it belongs to the current user
            // var order = await _context.Orders
            //     .FirstOrDefaultAsync(o => o.Order_id == id && o.User_id == userId);
              var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Order_id == id);

            if (order == null)
            {
                return NotFound("Order not found or you do not have permission to cancel this order.");
            }

            // Remove the order from the context
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Order successfully canceled." });
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Order_id == id);
        }
    }
}


