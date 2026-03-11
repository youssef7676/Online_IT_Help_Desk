using IT_Help_Desk.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IT_Help_Desk.Application.JwtService
{
    public class JwtTokens
    {
        private readonly IConfiguration _config;

        public JwtTokens(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            // Claims: info نضعها في التوكن
            var claims = new List<Claim>
            {
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                   new Claim(ClaimTypes.Name, user.FullName),

                   new Claim(ClaimTypes.Email, user.Email),

                   new Claim(ClaimTypes.Role, user.Role)

            };

            // المفتاح السري
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:DurationInHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}