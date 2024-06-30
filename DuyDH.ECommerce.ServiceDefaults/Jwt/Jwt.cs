namespace DuyDH.ECommerce.ServiceDefaults.Jwt;

public class Jwt
{
    public required string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}