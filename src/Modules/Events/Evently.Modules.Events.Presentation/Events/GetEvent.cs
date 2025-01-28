using Evently.Modules.Events.Application.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

public static class GetEvent
{
    internal static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id:guid}", async (Guid id, ISender sender) =>
            {
                var getEventQuery = new GetEventQuery(id);

                EventResponse @event = await sender.Send(getEventQuery);

                return @event is null
                    ? Results.NotFound()
                    : Results.Ok(@event);
            })
            .WithTags(Tags.Events);
    }
}
