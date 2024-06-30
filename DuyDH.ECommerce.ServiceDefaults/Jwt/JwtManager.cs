using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DuyDH.ECommerce.ServiceDefaults.Jwt;

public class JwtManager(IOptions<JwtConfiguration> jwtConfiguration, IConnectionMultiplexer connectionMultiplexer)
{
    private readonly int _accessTokenExpirationInSecond = 3600;
    private readonly IDatabase _redisDatabase = connectionMultiplexer.GetDatabase();
    public ServiceDefaults.Jwt.Jwt GenerateToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Value.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtConfiguration.Value.Issuer,
            audience: jwtConfiguration.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_accessTokenExpirationInSecond),
            signingCredentials: creds
        );
        
        return new ServiceDefaults.Jwt.Jwt
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = _accessTokenExpirationInSecond
        };
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
        {
            return false;
        }
        
        var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
        
        if (string.IsNullOrEmpty(jti))
        {
            return false;
        }

        
        var expirationTime = jwtToken.ValidTo;
        var remainingTime = expirationTime - DateTime.UtcNow;
        if (remainingTime.TotalSeconds <= 0)
        {
            return false;
        }
            
        var result = await _redisDatabase.StringSetAsync(jti, true, remainingTime);
        return result;
    }
    
    public async Task<bool> IsTokenRevoked(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return true;
        }
        var handler = new JwtSecurityTokenHandler();
        if (handler.ReadToken(token) is not JwtSecurityToken jwtToken)
        {
            return false;
        }

        var jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
        return !string.IsNullOrEmpty(jti) && await _redisDatabase.KeyExistsAsync(jti);
    }

}