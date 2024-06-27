using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.LoginUser;

public class LoginUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}