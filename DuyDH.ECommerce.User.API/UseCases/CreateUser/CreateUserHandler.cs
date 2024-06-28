using MediatR;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace DuyDH.ECommerce.User.API.UseCases.CreateUser;

public class CreateUserHandler(GraphServiceClient graphServiceClient) : IRequestHandler<CreateUserCommand, Microsoft.Graph.Models.User>
{
    public async Task<Microsoft.Graph.Models.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Microsoft.Graph.Models.User
        {
            AccountEnabled = true,
            DisplayName = request.Email.Split('@')[0],
            MailNickname = request.Email.Split('@')[0],
            Mail = request.Email,
            UserType = "Member",
            // UserPrincipalName = $"{request.Email.Split('@')[0]}@duydhecommerce.onmicrosoft.com",
            PasswordProfile = new Microsoft.Graph.Models.PasswordProfile
            {
                ForceChangePasswordNextSignIn = false,
                Password = request.Password
            },
            Identities = new List<Microsoft.Graph.Models.ObjectIdentity>
            {
                new()
                {
                    SignInType = "emailAddress",
                    Issuer = "duydhecommerce.onmicrosoft.com",
                    IssuerAssignedId = request.Email
                }
            }
        };

        try
        {
            var result = await graphServiceClient.Users
                .PostAsync(user, cancellationToken: cancellationToken);

            return result;
        }
        catch (ServiceException ex)
        {
            throw new ApplicationException($"Error creating user: {ex.Message}", ex);
        }
    }
}