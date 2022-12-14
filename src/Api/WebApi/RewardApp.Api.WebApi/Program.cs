using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using RewardApp.Api.Application.Extensions;
using RewardApp.ExceptionHandlingExtension.Extensions;
using RewardApp.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// man
builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);

var app = builder.Build();

// man
app.ConfigureExceptionHandling(opt =>
{
    var logger = app.Services.GetService<ILogger>();
    opt.UseExceptionDetails = true;
    opt.UseLogger(logger);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
