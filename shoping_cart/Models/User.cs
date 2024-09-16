using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shoping_cart.Models
{
    public class User
    {
        [Key]
        [Required]
        public int User_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string User_name { get; set; }

        [Required]
        [EmailAddress]
        public string User_email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "User";

        // Navigation property to related carts
       public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        // Navigation property to related orders
       public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
