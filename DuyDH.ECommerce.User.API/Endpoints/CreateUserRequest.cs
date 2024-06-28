using System.ComponentModel.DataAnnotations;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class CreateUserRequest
{
    public const string Route = "/";
    
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}