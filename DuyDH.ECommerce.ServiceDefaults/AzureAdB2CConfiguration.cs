namespace DuyDH.ECommerce.User.API.Configurations;

public class AzureAdB2CConfiguration
{
    public string Instance { get; set; }
    public string Domain { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string TenantId { get; set; }
    public string[] Scopes { get; set; }
}