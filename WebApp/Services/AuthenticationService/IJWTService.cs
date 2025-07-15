using WebApp.Models.Users;

namespace WebApp.Services.AuthenticationService;

public interface IJWTService
{
    string GenerateToken(User user);

    void SetJwtToken(HttpContext context, User user);

    void ClearJwtToken(HttpContext context);
}
