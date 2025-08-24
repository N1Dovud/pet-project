using System.Net.Http.Headers;

namespace WebApp.Helpers;

internal class JwtDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor accessor;

    public JwtDelegatingHandler(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var token = this.accessor.HttpContext?.Request.Cookies["jwt"];
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
