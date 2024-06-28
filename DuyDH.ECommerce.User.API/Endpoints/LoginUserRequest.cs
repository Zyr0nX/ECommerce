using System.ComponentModel.DataAnnotations;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class LoginUserRequest
{
    public const string Route = "/login";
    
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}