using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DuyDH.ECommerce.User.API.UseCases.GetUser;

public class GetUserHandler(UserManager<IdentityUser> userManager) : IRequestHandler<GetUserQuery, Result<IdentityUser>>
{
    public async Task<Result<IdentityUser>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        return user is null ? Result.NotFound() : Result.Success(user);
    }
}