using Closavy.Server.Data;
using Closavy.Server.HealthCheck;
using Closavy.Server.Services.Character;
using Closavy.Server.Validations.Character;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck");

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GameDatabase")));

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddValidatorsFromAssemblyContaining<CharacterCreateDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Closavy.Server v1");
    });
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
