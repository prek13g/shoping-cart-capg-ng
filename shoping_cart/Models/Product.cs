using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoping_cart.Models
{
    public class Product
    {
        [Key]
        public int Product_id { get; set; }

        [Required]
        public string Product_name { get; set; }

        public string Product_description { get; set; }

        public int Product_Quantity { get; set; }

        public decimal Product_Price { get; set; }

        // Foreign Key property
        public int Category_id { get; set; }

        [ForeignKey("Category_id")]
        public Category Category { get; set; } // Navigation to Category

        // Navigation property to related carts
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public string image {get; set; }
        
    }
}

