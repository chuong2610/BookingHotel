using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingHotel.Interfaces;
using BookingHotel.Models;
using Microsoft.IdentityModel.Tokens;


namespace BookingHotel.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration? _configuration;

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
    }
}