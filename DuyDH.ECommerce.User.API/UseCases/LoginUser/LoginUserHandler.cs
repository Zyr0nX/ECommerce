using DuyDH.ECommerce.User.API.UseCases.LoginUser;
using MediatR;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class LoginUserHandler(IConfidentialClientApplication graphServiceClient, ) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await graphServiceClient
            .ExecuteAsync();

        return Ok(new { Token = result.AccessToken });

        var user = new Microsoft.Graph.Models.User
        {
            AccountEnabled = true,
            Mail = request.Email,
            PasswordProfile = new Microsoft.Graph.Models.PasswordProfile
            {
                ForceChangePasswordNextSignIn = false,
                Password = request.Password
            }
        };

        try
        {
            var result = await graphServiceClient.Users
                .PostAsync(user, cancellationToken: cancellationToken);

            return result.Id;
        }
        catch (ServiceException ex)
        {
            throw new ApplicationException($"Error creating user: {ex.Message}", ex);
        }
    }
}