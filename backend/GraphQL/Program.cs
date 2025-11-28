using DotNetEnv;
using EfCoreModelsLib.Models.Mysql;
using GraphQL.Services;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;

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
       options.LogTo(Console.WriteLine);
       options.EnableSensitiveDataLogging();
       options.EnableDetailedErrors();
});

builder.Services.AddScoped<ITitlesService, TitlesService>();

builder.AddGraphQL()
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

app.RunWithGraphQLCommands(args);
