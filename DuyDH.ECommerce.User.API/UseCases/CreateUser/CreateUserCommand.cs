using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}