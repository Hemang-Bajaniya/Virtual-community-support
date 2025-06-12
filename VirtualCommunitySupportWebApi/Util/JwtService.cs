using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualCommunitySupportWebApi.Models;

namespace VirtualCommunitySupportWebApi.Util
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Emailaddress),
                new Claim(ClaimTypes.Name, user.Firstname+" "+user.Lastname),
                new Claim(ClaimTypes.Role, user.Usertype != null ? user.Usertype : "User"),
                 new Claim("userType", user.Usertype != null ? user.Usertype : "User"),
                 new Claim("userId", user.Id.ToString()),
                 new Claim("fullName", user.Firstname+" "+ user.Lastname),
                 new Claim("firstName", user.Firstname),
                 new Claim("lastName", user.Lastname)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audiance"],
                signingCredentials: cred,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
