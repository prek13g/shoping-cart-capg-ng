using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shoping_cart.Models
{
    public class Category
    {
        [Key]
        public int Category_id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Category_name { get; set; }

        // Navigation property to related products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}