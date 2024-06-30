using Ardalis.Result;
using DuyDH.ECommerce.ServiceDefaults;
using DuyDH.ECommerce.ServiceDefaults.Jwt;
using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.LogoutUser;

public class LogoutUserHandler(JwtManager jwtManager) : IRequestHandler<LogoutUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        return await jwtManager.RevokeTokenAsync(request.Token);
    }
}