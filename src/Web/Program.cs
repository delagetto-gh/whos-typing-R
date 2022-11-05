using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApp();
builder.Services.AddInfra();

var app = builder.Build();

app.UseApp();
app.UseFileServer();

app.Run();
