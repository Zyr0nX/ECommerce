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
        var confidentialClientApplication = sp.GetRequiredService<IConfidentialClientApplication>();
        var provider = new TokenProvider(confidentialClientApplication);
        var authenticationProvider = new BaseBearerTokenAuthenticationProvider(provider);
        return new GraphServiceClient(authenticationProvider);
    });

    }
    
    public static void AddMsalClient(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<AzureAdB2CConfiguration>(configuration.GetSection("AzureAdB2C"));
        services.AddSingleton<IPublicClientApplication>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAdB2CConfiguration>>().Value;
            return PublicClientApplicationBuilder.Create(options.ClientId)
                .WithTenantId(options.TenantId)
                .WithB2CAuthority("https://duydhecommerce.b2clogin.com/tfp/duydhecommerce.onmicrosoft.com/B2C_1_SignUpSignIn")
                .Build();
        });
        services.AddSingleton<IConfidentialClientApplication>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureAdB2CConfiguration>>().Value;
            return ConfidentialClientApplicationBuilder.Create(options.ClientId)
                .WithClientSecret(options.ClientSecret)
                .WithTenantId(options.TenantId)
                .Build();
        });
    }
}