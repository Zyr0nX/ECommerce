using System.Net.Http.Headers;
using System.Text;
using DuyDH.ECommerce.ServiceDefaults.Jwt;
using DuyDH.ECommerce.User.API.Data;
using DuyDH.ECommerce.User.API.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Kiota.Abstractions.Authentication;

namespace DuyDH.ECommerce.User.API.Extensions;

public static class ServiceExtensions
{
    public static void AddUserDbContext(
        this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<UserDbContext>("postgres");
        builder.EnrichNpgsqlDbContext<UserDbContext>();
    } 
}