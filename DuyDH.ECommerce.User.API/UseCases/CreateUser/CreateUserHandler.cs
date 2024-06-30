using System.Security.Claims;
using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserHandler(UserManager<IdentityUser> userManager) : IRequestHandler<CreateUserCommand, Result<IdentityUser>>
{
    public async Task<Result<IdentityUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return Result.Error(new ErrorList(result.Errors.Select(x => x.Description)));
        }

        var createdUser = await userManager.FindByEmailAsync(request.Email);
        Guard.Against.Null(createdUser);
        return Result.Success(createdUser);
    }
}