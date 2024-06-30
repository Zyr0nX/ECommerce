using DuyDH.ECommerce.User.API.Data;
using DuyDH.ECommerce.User.Migration;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddNpgsqlDbContext<UserDbContext>("userDb");
builder.EnrichNpgsqlDbContext<UserDbContext>();

var host = builder.Build();
host.Run();