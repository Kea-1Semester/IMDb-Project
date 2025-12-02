using DotNetEnv;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Auth0;
using GraphQL.Handler;
using GraphQL.Repos;
using GraphQL.Services;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var getEnv = GetEnvironmentVariable.GetEnvVar;
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
    options.UseMySql(Env.GetString("MySqlConnectionString"),
        ServerVersion.AutoDetect(Env.GetString("MySqlConnectionString")));
    if (builder.Environment.IsDevelopment())
    {
        options.LogTo(Console.WriteLine);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddTransient<ITitlesRepo, TitlesRepo>();
builder.Services.AddTransient<ITitlesService, TitlesService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{getEnv("Auth0Domain")}";
        options.Audience = getEnv("Auth0Audience");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            ValidIssuer = $"https://{getEnv("Auth0Domain")}/",
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(getEnv("IssuerSigningKey")))
        };
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
    });


builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(AuthPolicy.ReadPolicies, policy => policy.Requirements.Add(
            new GraphQL.Auth0.HasPermissionRequirement(AuthPolicy.ReadPolicies, getEnv("Auth0Domain"))
        ))
    .AddPolicy(AuthPolicy.WritePolicies, policy => policy.Requirements.Add(
            new GraphQL.Auth0.HasPermissionRequirement(AuthPolicy.WritePolicies, getEnv("Auth0Domain"))
        ))
    .AddPolicy(AuthPolicy.DeletePolicies, policy => policy.Requirements.Add(
            new GraphQL.Auth0.HasPermissionRequirement(AuthPolicy.DeletePolicies, getEnv("Auth0Domain"))
        ))
    .AddPolicy(AuthPolicy.UpdatePolicies, policy => policy.Requirements.Add(
            new GraphQL.Auth0.HasPermissionRequirement(AuthPolicy.UpdatePolicies, getEnv("Auth0Domain"))
        )
    );

builder.Services.AddSingleton<IAuthorizationHandler, HasPermissionHandler>();


builder.AddGraphQL()
    .AddAuthorization()
    .AddTypes()
    .AddSorting()
    .AddFiltering()
    .AddProjections()
    .ModifyRequestOptions(o => o.IncludeExceptionDetails =
        builder.Environment.IsDevelopment())
    .DisableIntrospection(!builder.Environment.IsDevelopment());

var app = builder.Build();

app.MapGraphQL().WithOptions(new GraphQLServerOptions()
{
    EnableGetRequests = false
});

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.RunWithGraphQLCommands(args);



