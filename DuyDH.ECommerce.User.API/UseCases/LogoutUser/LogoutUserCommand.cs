using Ardalis.Result;
using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.LogoutUser;

public class LogoutUserCommand : IRequest<Result<bool>>
{
    public required string Token { get; set; }
}