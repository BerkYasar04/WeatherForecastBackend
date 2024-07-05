using System.Net;
using WebAPI.Business.Manager;
using WebAPI.Business.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMGMService, MGMManager>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins(builder.Configuration.GetSection("AllowedOrigin").Get<string[]>() ?? ["*"])
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All,
    RequireHeaderSymmetry = false,
    ForwardLimit = null,
    KnownProxies = { IPAddress.Parse("127.0.0.1") }
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
