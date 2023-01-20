using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RewardApp.Api.Application.Extensions;
using RewardApp.ExceptionHandlingExtension.Extensions;
using RewardApp.Infrastructure.Persistence.Extensions;
using Serilog;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddServer(new OpenApiServer()
    {
        Url = "https://31.187.74.32"
    });
});

// man
builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);


builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
});

// test 
//builder.Services.AddLogging();

var app = builder.Build();

// man
app.ConfigureExceptionHandling(opt =>
{
    var logger = app.Services.GetService<Serilog.ILogger>();
    opt.UseExceptionDetails = true;
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
