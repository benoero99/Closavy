using Closavy.Server.Data;
using Closavy.Server.Exceptions;
using Closavy.Server.HealthCheck;
using Closavy.Server.Services.Account;
using Closavy.Server.Services.Auth;
using Closavy.Server.Services.Character;
using Closavy.Server.Validations.Character;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails(
    configure =>
{
    configure.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
        context.ProblemDetails.Extensions.TryAdd("title", context.ProblemDetails.Title);
        context.ProblemDetails.Extensions.TryAdd("detail", context.ProblemDetails.Detail);
        context.ProblemDetails.Extensions.TryAdd("type", context.ProblemDetails.Type);
    };
}
);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecks().AddCheck<HealthCheck>("HealthCheck");

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GameDatabase")));

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentAccountService, DevelopmentCurrentAccountService>();
builder.Services.AddValidatorsFromAssemblyContaining<CharacterCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AccountCreateDtoValidator>();

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

//Add exception handler in the middleware
app.UseExceptionHandler();

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
