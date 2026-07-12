using LoadTestingSystem.API.Data;
using Microsoft.AspNetCore.Mvc;
using LoadTestingSystem.API.DTOs;

namespace LoadTestingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == request.Login && u.PasswordHash == request.Password);
            if(user == null)
                return Unauthorized();
            
            return Ok();
        }
    }
}
