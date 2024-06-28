using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserCommand : IRequest<Microsoft.Graph.Models.User>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}