using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserCommand : IRequest<Result<IdentityUser>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}