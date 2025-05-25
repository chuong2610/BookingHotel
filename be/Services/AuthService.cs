using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingHotel.Interfaces;
using BookingHotel.Models;
using BookingHotel.Models.Request;
using BookingHotel.Request;
using Microsoft.IdentityModel.Tokens;



namespace BookingHotel.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public AuthService(IConfiguration configuration, IUserRepository userRepository, IEmailService emailService)
        {
            _emailService = emailService;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        private string GenerateToken(User user)
        {
            var securityKey = _configuration["Jwt:Key"];
            var formatKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(formatKey, SecurityAlgorithms.HmacSha256);
            var clamims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
           };
            var token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Audience"],
                 claims: clamims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(LoginRequest loginRequest)
        {
            // Validate the user credentials
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return null;
            }
            return GenerateToken(user);
        }

        public async Task<User> CreateUserAsync(RegisterRequest user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string encodedToken = Uri.EscapeDataString(token);
            var baseUrl = _configuration["AppSettings:BaseUrl"];
            var verifyUrl = $"{baseUrl}/api/Auth/verify-email?token={encodedToken}";

            var newUser = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password, // Ensure to hash the password in a real application
                VerificationToken = token,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow,
                RoleId = 1 // 1 is customer

            };
            await _userRepository.CreateUserAsync(newUser);
            var emailBody = $@"
                <h1>Welcome to Our Service</h1>
                <p>Dear {user.FullName},</p>
                <p>Thank you for registering with us. We are excited to have you on board!</p>
                <p>If you have any questions, feel free to reach out.</p>
                <a href='{verifyUrl}'>Click here to verify your email</a>
                <p>Best regards,</p>
                <p>Your Company Name</p>
            ";
            await _emailService.SendEmailAsync(user.Email, "Welcome to Our Service", emailBody);
            return newUser;
        }
         public async Task<User?> VerifyEmailAsync(string token)
        {
           return await _userRepository.GetUserByVerificationTokenAsync(token);
        }
        
    }
}