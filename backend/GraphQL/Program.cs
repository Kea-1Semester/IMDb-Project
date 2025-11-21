using HotChocolate.AspNetCore;

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

builder.AddGraphQL()
       .AddTypes()
       .AddSorting()
       .AddFiltering();

var app = builder.Build();

app.MapGraphQL().WithOptions(new GraphQLServerOptions()
{
       EnableGetRequests = false
});

app.UseCors();

app.RunWithGraphQLCommands(args);
