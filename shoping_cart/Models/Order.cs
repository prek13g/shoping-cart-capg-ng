using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoping_cart.Models
{
    public class Order
    {
        [Key]
        public int Order_id { get; set; }

        [Required]
        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; }

        [Required]
        public int Product_id { get; set; }

        [ForeignKey("Product_id")]
        public Product Product { get; set; }

        [Required]
        public DateTime OrderDateTime { get; set; }

        public int Product_Quantity { get; set; }

        // [Required(ErrorMessage = "Product Price is required")]
        // [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Product_Price { get; set; }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero")]
        public decimal Product_TotalAmount { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

    }
}

