using Microsoft.EntityFrameworkCore;
using WebApi.Business;
using WebApi.Mappers;
using WebApi.Services.Database;

namespace WebApi.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public long? Authenticate(long id)
    {
        return id;
    }
}
