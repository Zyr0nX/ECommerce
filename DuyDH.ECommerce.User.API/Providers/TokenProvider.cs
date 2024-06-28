using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions.Authentication;

namespace DuyDH.ECommerce.User.API.Providers;

public class TokenProvider(IConfidentialClientApplication confidentialClientApplication) : IAccessTokenProvider
{
    public async Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object>? additionalAuthenticationContext = default,
        CancellationToken cancellationToken = default)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };
        var result = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync(cancellationToken);

        // get the token and return it
        return result.AccessToken;
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }
}