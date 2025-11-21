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

app.MapGraphQL();

app.UseCors();

app.RunWithGraphQLCommands(args);
