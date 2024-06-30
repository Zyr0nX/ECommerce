using Ardalis.GuardClauses;
using DuyDH.ECommerce.User.API.UseCases.LogoutUser;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class Logout(IMediator mediator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(LoginUserRequest.Route);
    }

    public override async Task HandleAsync(
        CancellationToken cancellationToken)
    {
        var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
        Guard.Against.Null(token);
        var result = await mediator.Send(new LogoutUserCommand
        {
            Token = token
        }, cancellationToken);
    }
}