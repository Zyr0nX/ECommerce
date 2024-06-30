using System.IdentityModel.Tokens.Jwt;
using Ardalis.Result;
using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.GetEmail;

public class GetEmailHandler : IRequestHandler<GetEmailQuery, Result<string>>
{
    public Task<Result<string>> Handle(GetEmailQuery request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.ReadToken(request.Token) is not JwtSecurityToken jwtToken)
        {
            return Task.FromResult(Result<string>.Invalid());
        }
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
        return emailClaim == null ? Task.FromResult(Result<string>.Error()) : Task.FromResult(Result.Success(emailClaim.Value));
    }
}