using api.Policies;
using application;
using core_infrastructure;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddCoreInfrastructure();
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = builder.Configuration.GetValue<string>("IdentityServer:BaseUrl");
                    options.ApiName = "resource-api";
                    options.JwtValidationClockSkew = TimeSpan.Zero;
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = SupportedTokens.Jwt;
                });


builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("WeatherForecastFor5DaysPolicy", policy => policy.Requirements.Add(new WeatherForecastFor5DaysPolicyRequirement()));

    option.AddPolicy("WeatherForecastFor10DaysPolicy", policy => policy.Requirements.Add(new WeatherForecastFor10DaysPolicyRequirement()));

    option.AddPolicy("WeatherForecastForInstantPolicy", policy => policy.Requirements.Add(new WeatherForecastForInstantPolicyRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, WeatherForecastForInstantPolicyHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, WeatherForecastFor5DaysPolicyHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, WeatherForecastFor10DaysPolicyHandler>();

builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
