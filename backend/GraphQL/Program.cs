var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL()
       .AddTypes()
       .AddSorting()
       .AddFiltering();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
