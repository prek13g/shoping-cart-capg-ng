using System.ComponentModel.DataAnnotations;
namespace Shoping_cart.DTOs
{
    public class CartDTO
    {
        public int Cart_id { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public int User_id { get; set; }
        [Required(ErrorMessage = "Product ID is required")]
        public int Product_id { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }

        // Optionally include other related information as needed
        // For example, you might want to include the product name or user name
        // but this depends on your requirements and whether you want to expose those details
        // public string Product_name { get; set; }
        // public string User_name { get; set; }
    }
}

