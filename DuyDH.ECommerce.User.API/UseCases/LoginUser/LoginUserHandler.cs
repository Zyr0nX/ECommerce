﻿using System.Net;
using DuyDH.ECommerce.User.API.Configurations;
using DuyDH.ECommerce.User.API.UseCases.LoginUser;
using MediatR;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class LoginUserHandler(IPublicClientApplication msalClient, AzureAdB2CConfiguration configuration)
    : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await msalClient.AcquireTokenByUsernamePassword(configuration.Scopes, request.Email,
            request.Password).ExecuteAsync(cancellationToken);
        return result.AccessToken;
    }
}