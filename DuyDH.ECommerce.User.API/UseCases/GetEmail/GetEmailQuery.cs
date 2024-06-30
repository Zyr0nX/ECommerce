using Ardalis.Result;
using MediatR;

namespace DuyDH.ECommerce.User.API.UseCases.GetEmail;

public class GetEmailQuery : IRequest<Result<string>>
{
    public required string Token { get; set; }
}