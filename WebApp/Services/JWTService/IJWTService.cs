using WebApp.Models;

namespace WebApp.Services.JWTService;

public interface IJWTService
{
    string GenerateToken(User user);

    void SetJwtToken(HttpContext context, User user);

    void ClearJwtToken(HttpContext context);
}
