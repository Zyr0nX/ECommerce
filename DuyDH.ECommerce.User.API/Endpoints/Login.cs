using Ardalis.GuardClauses;
using DuyDH.ECommerce.User.API.UseCases.LoginUser;
using FastEndpoints;
using MediatR;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class Login(IMediator mediator) : Endpoint<LoginUserRequest>
{
    public override void Configure()
    {
        Post(LoginUserRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken)
    {
        Guard.Against.NullOrWhiteSpace(request.Email);
        Guard.Against.NullOrWhiteSpace(request.Password);
        var result = await mediator.Send(new LoginUserCommand()
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken);
    }
}