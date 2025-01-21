using Evently.Api.Extensions;
using Evently.Modules.Events.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.ApplyMigrations();
}

app.MapGet("/", () => "Hello World!");

EventsModule.MapEndpoints(app);

app.Run();
