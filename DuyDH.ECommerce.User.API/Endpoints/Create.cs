using DuyDH.ECommerce.ServiceDefaults;
using MediatR;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("create", async (string email, string password, IMediator mediator) =>
        {
            return Results.Ok();
            
        })
    }
}