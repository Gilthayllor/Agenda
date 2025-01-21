namespace Evently.Modules.Events.Domain.Events;

#pragma warning disable CA1716
public sealed class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartAtUtc { get; set; }
    public DateTime? EndsAtUtc { get; set; }
    public EventStatus Status { get; set; }
}
