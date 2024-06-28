using Microsoft.Kiota.Abstractions.Authentication;

namespace DuyDH.ECommerce.User.API.Providers;

public class TokenProvider : IAccessTokenProvider
{
    public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object>? additionalAuthenticationContext = default,
        CancellationToken cancellationToken = default)
    {
        var token = "token";
        // get the token and return it
        return Task.FromResult(token);
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }
}