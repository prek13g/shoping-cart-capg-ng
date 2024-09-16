using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Password_Hash.services;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shoping_cart.Models;

namespace Shoping_cart.Controllers
{
    [Route("api/[controller]/Login")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly Password_Hasher _password_Hash;
        private readonly IConfiguration _configuration;
        public AdminController(DBContext context, Password_Hasher passwordHasher, IConfiguration configuration)
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
            var user = await _context.Admins
                    .FirstOrDefaultAsync(u => u.AdminEmail == loginDto.User_email);
            if (user != null)
            {
                // Verify the provided password with the stored hashed password
                bool isPasswordValid = _password_Hash.VerifyPassword(user.AdminPassword, loginDto.Password);

                if (isPasswordValid)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("User_name",user.AdminName.ToString()),
                    new Claim("User_email",user.AdminEmail.ToString()),
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
                    return Ok(new { Token = tokenValue, username = user.AdminName ,useremail=user.AdminEmail,role=user.Role });
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

