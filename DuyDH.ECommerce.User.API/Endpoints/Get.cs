using Ardalis.GuardClauses;
using DuyDH.ECommerce.User.API.UseCases.GetEmail;
using DuyDH.ECommerce.User.API.UseCases.GetUser;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class GetUser(IMediator mediator) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(GetUserRequest.Route);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
        Guard.Against.Null(token);
        var email = await mediator.Send(new GetEmailQuery
        {
            Token = token
        }, cancellationToken);
        
        var result = await mediator.Send(new GetUserQuery
        {
            Email = email
        }, cancellationToken);
        await SendOkAsync(result, cancellationToken);
    }
}