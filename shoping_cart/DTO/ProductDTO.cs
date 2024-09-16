using System.ComponentModel.DataAnnotations;
namespace Shoping_cart.DTOs
{
    public class ProductDTO
    {
        public int Product_id { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Product name must be between 4 and 100 characters")]
        public string Product_name { get; set; }
        [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters")]
        public string Product_description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Product quantity must be a positive integer")]
        public int Product_Quantity { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero")]
        public decimal Product_Price { get; set; }
        [Required(ErrorMessage = "Category id is required")]
        public int Category_id { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string image {get; set; }
    }
}

