using System.Data.Common;
using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using MediatR;

namespace Evently.Modules.Events.Application.Events;

public sealed record GetEventQuery(Guid EventId) : IRequest<EventResponse?>;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                id as {nameof(EventResponse.Id)},
                title as {nameof(EventResponse.Title)},
                description as {nameof(EventResponse.Description)},
                location as {nameof(EventResponse.Location)},
                start_at_utc as {nameof(EventResponse.StartAtUtc)},
                ends_at_utc as {nameof(EventResponse.EndsAtUtc)}
             WHERE id = @EventId
             """; // SQL query to get event by id
        
        EventResponse? @event = await connection.QueryFirstOrDefaultAsync<EventResponse>(sql, request);

        return @event;
    }
}
