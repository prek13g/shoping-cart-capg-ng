
using System.ComponentModel.DataAnnotations;
namespace Shoping_cart.DTOs
{
    public class UserDTO
    {
        public int User_id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string User_name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string User_email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be at least 6 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        //public string Role { get; set; } = "User";

        // Exclude password for security reasons
    }
}

