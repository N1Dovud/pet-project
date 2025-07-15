using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApp.Models.Users;

namespace WebApp.Services.AuthenticationService;

public class JWTService(IConfiguration configuration) : IJWTService
{
    public void ClearJwtToken(HttpContext context)
    {
        context?.Response.Cookies.Delete("jwt");
    }

    public string GenerateToken(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("userId", user.Id.ToString(CultureInfo.InvariantCulture)),
        };

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToDouble(configuration["JwtSettings:ExpiryHours"], CultureInfo.InvariantCulture)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void SetJwtToken(HttpContext context, User user)
    {
        var token = this.GenerateToken(user);
        var expiryTime = configuration.GetValue<int>("JwtSettings:ExpiryHours");
        context?.Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false,
            Expires = DateTime.Now.AddHours(expiryTime),
        });
    }
}
