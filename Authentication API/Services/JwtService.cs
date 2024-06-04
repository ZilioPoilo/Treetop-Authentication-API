using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication_API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public bool ValidateRefreshToken(string token, string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["RefreshJWT:Issuer"],
                ValidAudience = _config["RefreshJWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["RefreshJWT:Key"]!))
            };

            if (!tokenHandler.CanReadToken(token))
                return false;

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (principal.HasClaim(c => c.Type == "UserId"))
                    return userId == principal.Claims.First(c => c.Type == "UserId").Value;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Refresh token validation error: @1", exception);
            }

            return false;
        }

        public string GenerateAuthorizationToken(string userId, string role, int minutes = 60)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthorizeJWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", userId),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _config["AuthorizeJWT:Issuer"],
                _config["AuthorizeJWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["RefreshJWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", userId)
            };

            var token = new JwtSecurityToken(
                _config["RefreshJWT:Issuer"],
                _config["RefreshJWT:Audience"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
