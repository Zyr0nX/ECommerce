using Ardalis.Result;
using DuyDH.ECommerce.ServiceDefaults.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DuyDH.ECommerce.User.API.UseCases.LoginUser;

public class LoginUserHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JwtManager jwtManager)
    : IRequestHandler<LoginUserCommand, Result<Jwt>>
{
    public async Task<Result<Jwt>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result.Error("User not found");
        
        var result = await signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);
        
        if (!result.Succeeded)
            return Result.Error("Wrong password");
        
        var token = jwtManager.GenerateToken(user);
        return Result.Success(token);
    }
}