using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.Models;
using System.Linq;
using System.Threading.Tasks;
namespace Shoping_cart.Services
{
    public class CartService
    {
        private readonly DBContext _context;
        public CartService(DBContext context)
        {
            _context = context;
        }
        public async Task RemoveProductFromCartAsync(int userId, int productId)
        {
            // Find the cart item matching the user ID and product ID
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.User_id == userId && c.Product_id == productId);

            if (cartItem == null)
            {
                throw new KeyNotFoundException("Cart item or product not found.");
            }

            // Remove the item from the cart
            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}









