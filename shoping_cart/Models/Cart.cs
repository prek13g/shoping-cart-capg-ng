using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoping_cart.Models
{
    public class Cart
    {
        [Key]
        public int Cart_id { get; set; }

        [Required]
        public int User_id { get; set; }

        [ForeignKey("User_id")]
        public User User { get; set; }

        [Required]
        public int Product_id { get; set; }

        [ForeignKey("Product_id")]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
