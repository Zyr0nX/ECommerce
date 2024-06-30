using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DuyDH.ECommerce.User.API.UseCases.GetUser;

public class GetUserQuery : IRequest<Result<IdentityUser>>
{
    public required string Email { get; set; }
}