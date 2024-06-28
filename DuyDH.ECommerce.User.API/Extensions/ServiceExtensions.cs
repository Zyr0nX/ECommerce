using System.Net.Http.Headers;
using DuyDH.ECommerce.User.API.Configurations;
using DuyDH.ECommerce.User.API.Providers;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions.Authentication;

namespace DuyDH.ECommerce.User.API.Extensions;

public static class ServiceExtensions
{
    public static void AddGraphServiceClient(
        this IServiceCollection services)
    {
        services.AddSingleton<GraphServiceClient>(sp =>
    {
        var msalClient = sp.GetRequiredService<IConfidentialClientApplication>();
        var authProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider());

        return new GraphServiceClient(authProvider);
    });

    }
    
    public static void AddMSALClient(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AzureAdB2CConfiguration>(configuration.GetSection("AzureAdB2C"));
        services.AddSingleton<IConfidentialClientApplication>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAdB2CConfiguration>>().Value;
            return ConfidentialClientApplicationBuilder.Create(options.ClientId)
                .WithClientSecret(options.ClientSecret)
                .WithAuthority(new Uri($"{options.Instance}/{options.TenantId}/v2.0"))
                .Build();
        });
    }
}