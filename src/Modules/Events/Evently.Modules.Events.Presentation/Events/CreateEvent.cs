using Evently.Modules.Events.Application.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

public static class CreateEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) =>
            {
                Guid id = await sender.Send(new CreateEventCommand(
                    request.Title,
                    request.Description,
                    request.Location,
                    request.StartAtUtc,
                    request.EndsAtUtc
                ));

                return Results.Ok(id);
            })
            .WithTags(Tags.Events);
    }

    internal sealed class Request
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }
    }
}
