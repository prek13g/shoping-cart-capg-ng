using System.ComponentModel.DataAnnotations;
namespace Shoping_cart.DTO
{
    public class CategoryDto
    {
        public int Category_id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters")]
        public string Category_name { get; set; }
    }
}