using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DTOs;
using Shoping_cart.Models;
using Shoping_cart.DTO;
using Password_Hash.services;
using Shoping_cart.DatabaseContext;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Shoping_cart.Controllers
{
    [Route("api/User/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly Password_Hasher _password_Hash;
        private readonly IConfiguration _configuration;
        public LoginController(DBContext context, Password_Hasher passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _password_Hash = passwordHasher;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostLogin([FromBody] LoginDTO loginDto)
        {


            if (loginDto == null)
            {
                return BadRequest("User data is null");
            }

            // Fetch the user based on email
            var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.User_email == loginDto.User_email);
            if (user != null)
            {
                // Verify the provided password with the stored hashed password
                bool isPasswordValid = _password_Hash.VerifyPassword(user.Password, loginDto.Password);

                if (isPasswordValid)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("User_name",user.User_name.ToString()),
                    new Claim("User_email",user.User_email.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn
                    );
                    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                          return Ok(new {
                          Token = tokenValue,
                Username = user.User_name,
                UserId = user.User_id,
                UserEmail = user.User_email,
                UserRole = user.Role
                         });
                }
                else
                {
                    return Unauthorized("Invalid email or password");
                }

            }
            else
            {
                return Unauthorized("Invalid email or password");
            }


        }

    }
}
/*public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher _passwordHasher;
    public LoginController(ApplicationDbContext context, PasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost]
    public async Task<ActionResult<string>> PostLogin([FromBody] LoginDTO loginDto)
    {

        if (loginDto == null)
        {
            return BadRequest("User data is null");
        }
        // Fetch the user based on email
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.User_email == loginDto.User_email);
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }
        // Verify the provided password with the stored hashed password
        bool isPasswordValid = _passwordHasher.VerifyPassword(user.Password, loginDto.Password);

        if (isPasswordValid)
        {
            return Ok($"Successfully logged in {user.User_name}");
        }
        return Unauthorized("Invalid email or password");

    }

} */
//}

