using WebApi.Business;

namespace WebApi.Services.Authentication;

public interface IAuthenticationService
{
    long? Authenticate(long id);
}
