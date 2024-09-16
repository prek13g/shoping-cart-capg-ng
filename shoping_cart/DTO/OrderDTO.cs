using System.ComponentModel.DataAnnotations;
namespace Shoping_cart.DTOs
{
    public class OrderDTO
    {
        public int Order_id { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public int User_id { get; set; }
        [Required(ErrorMessage = "Product ID is required")]
        public int Product_id { get; set; }
        [Required(ErrorMessage = "Order DateTime is required")]
        public DateTime OrderDateTime { get; set; }
        // [Required(ErrorMessage = "Total amount is required")]
        // [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]

        [Required(ErrorMessage = "Product Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Product_Quantity { get; set; }

        [Required(ErrorMessage = "Product Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Product_Price { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]
        public decimal Product_TotalAmount { get; set; }

        [Required]
        public string Status { get; set; }
    }
}