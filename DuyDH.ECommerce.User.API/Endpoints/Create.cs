﻿using Ardalis.GuardClauses;
using DuyDH.ECommerce.ServiceDefaults;
using DuyDH.ECommerce.User.API.UseCases.CreateUser;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DuyDH.ECommerce.User.API.Endpoints;

public class Create(IMediator mediator) : Endpoint<CreateUserRequest>
{
    public override void Configure()
    {
        Post(CreateUserRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        Guard.Against.NullOrWhiteSpace(request.Email);
        Guard.Against.NullOrWhiteSpace(request.Password);
        var result = await mediator.Send(new CreateUserCommand
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken);
    }
}