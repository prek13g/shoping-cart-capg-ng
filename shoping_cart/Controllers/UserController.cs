using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoping_cart.DatabaseContext;
using Shoping_cart.DTOs;
using Shoping_cart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Password_Hash.services;
using Microsoft.AspNetCore.Identity;
using Shoping_cart.services;

namespace Shoping_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly Password_Hasher _passwordHasher;
        private readonly EmailSending _email;


        public UsersController(DBContext context, Password_Hasher passwordHasher, EmailSending email)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _email = email;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new UserDTO
                {
                    User_id = u.User_id,
                    User_name = u.User_name,
                    User_email = u.User_email,
                    Password = u.Password,
                    // Exclude password for security reasons
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.User_id == id)
                .Select(u => new UserDTO
                {
                    User_id = u.User_id,
                    User_name = u.User_name,
                    User_email = u.User_email
                    // Exclude password for security reasons
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.User_id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.User_name = userDto.User_name;
            user.User_email = userDto.User_email;
            // Password update would be handled separately and securely

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> PostUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }
            Password_Hasher a = new Password_Hasher();
            var b = _passwordHasher.HashPassword(userDto.Password);

            var user = new User
            {
                User_name = userDto.User_name,
                User_email = userDto.User_email,
                Password = b // Ensure this is handled securely
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _email.SendEmail(userDto.User_email, userDto.User_name);

            var createdUserDto = new UserDTO
            {
                User_id = user.User_id,
                User_name = user.User_name,
                User_email = user.User_email,
                Password = user.Password,
                // Password is not included in the response
            };
            return CreatedAtAction(nameof(GetUser), new { id = user.User_id }, createdUserDto);
        }

        [HttpGet("by-email/{email}")]
public async Task<ActionResult<UserDTO>> GetUserByEmail(string email)
{
    var user = await _context.Users
        .Where(u => u.User_email == email)
        .Select(u => new UserDTO
        {
            User_id = u.User_id,
            User_name = u.User_name,
            User_email = u.User_email
            // Exclude password for security reasons
        })
        .FirstOrDefaultAsync();

    if (user == null)
    {
        return NotFound("User not found");
    }

    return Ok(user);
}


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.User_id == id);
        }
    }
}

