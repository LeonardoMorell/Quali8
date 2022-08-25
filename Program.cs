using System.Reflection;
using FluentValidation.AspNetCore;
using Domain.Services.Interfaces;
using Domain.Services.Services;
using Application.Services;
using Application.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = new[] { Assembly.Load("Application") };
builder.Services.AddControllers();

builder.Services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(assemblies.First()));
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICustomerServices, CustomerServices>();
builder.Services.AddTransient<ICustomerApplicationService, CustomerApplicationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper((_, config) => config.AddMaps(assemblies), assemblies);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();