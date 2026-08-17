using IdentityApi.Data;
using IdentityApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// O SQLite não cria a pasta do arquivo do banco automaticamente
Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "db"));

builder.Services.AddInfrastructure(builder.Configuration);

// Diz quem você é
builder.Services.AddAuthentication();
// Diz o que você pode fazer
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference().AllowAnonymous();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

app.Run();
