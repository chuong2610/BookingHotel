using BookingHotel.Interfaces;
using BookingHotel.Models.Request;
using BookingHotel.Request;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null)
                    return BadRequest("Request body is null");
                var token = await _authService.Login(loginRequest);
                if (token == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            try
            {
                var verifiedUser = await _authService.VerifyEmailAsync(token);
                if (verifiedUser == null)
                {
                    return NotFound("Invalid or expired token.");
                }
                return Ok("Email verified successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try{
            var createdUser = await _authService.CreateUserAsync(registerRequest);
            return Ok(createdUser);
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
           
        }
    }
}