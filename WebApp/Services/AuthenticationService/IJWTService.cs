using WebApp.Models.Users;

namespace WebApp.Services.AuthenticationService;

internal interface IJwtService
{
    string GenerateToken(User user);

    void SetJwtToken(HttpContext context, User user);

    void ClearJwtToken(HttpContext context);
}
