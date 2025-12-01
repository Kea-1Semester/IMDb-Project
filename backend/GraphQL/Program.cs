using DotNetEnv;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Services;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using GraphQL.Handler;


Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContextFactory<ImdbContext>(options =>
{
    options.UseMySql(Env.GetString("MySqlConnectionString"), ServerVersion.AutoDetect(Env.GetString("MySqlConnectionString")));
    if (builder.Environment.IsDevelopment())
    {
        options.LogTo(Console.WriteLine);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddScoped<ITitlesService, TitlesService>();



var domain = GetEnvironmentVariable.GetEnvVar("Auth0Domain");


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = GetEnvironmentVariable.GetEnvVar("Auth0Audience");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            ValidIssuer = GetEnvironmentVariable.GetEnvVar("Auth0Domain"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetEnvironmentVariable.GetEnvVar("IssuerSigningKey")))


        };
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
    });

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("read:messages", policy => policy.Requirements.Add(
                new GraphQL.Auth0.HasScopeRequirement("read:messages", domain)
            )
);

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


builder.AddGraphQL()
    .AddAuthorization()
    .AddTypes()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .ModifyRequestOptions(
    o => o.IncludeExceptionDetails =
        builder.Environment.IsDevelopment());

var app = builder.Build();

app.MapGraphQL().WithOptions(new GraphQLServerOptions()
{
    EnableGetRequests = false
});

app.UseCors();

app.UseAuthentication();
//app.UseAuthorization();

app.RunWithGraphQLCommands(args);



