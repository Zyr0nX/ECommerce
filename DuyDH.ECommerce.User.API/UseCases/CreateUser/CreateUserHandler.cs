using MediatR;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserHandler(GraphServiceClient graphServiceClient) : IRequestHandler<CreateUserCommand, string>
{
    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
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