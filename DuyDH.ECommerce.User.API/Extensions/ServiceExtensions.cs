using Microsoft.Identity.Client;

namespace DuyDH.ECommerce.User.API.Extensions;

public static class ServiceExtensions
{
    public static void AddGraphServiceClient(
        this IServiceCollection services)
    {
        services.AddSingleton<IConfidentialClientApplication>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return ConfidentialClientApplicationBuilder
                .Create(configuration["AzureAdB2C:ClientId"])
                .WithClientSecret(configuration["AzureAdB2C:ClientSecret"])
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{configuration["AzureAdB2C:TenantId"]}"))
                .Build();
        });
    }
}