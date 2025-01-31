using Evently.Api.Extensions;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Evently";
    configure.Version = "v1";
    configure.Description = "Evently API";
});

builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
    app.ApplyMigrations();

    app.MapGet("/", (context) =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

EventsModule.MapEndpoints(app);

app.Run();
