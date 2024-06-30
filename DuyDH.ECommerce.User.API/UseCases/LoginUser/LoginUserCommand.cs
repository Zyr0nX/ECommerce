using Ardalis.Result;
using DuyDH.ECommerce.ServiceDefaults.Jwt;
using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.LoginUser;

public class LoginUserCommand : IRequest<Result<Jwt>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}