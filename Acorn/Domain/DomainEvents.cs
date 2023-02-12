using MediatR;

namespace Acorn.Domain;
public static class DomainEvents
{
    public static Func<IPublisher> Publisher { get; set; }
    public static async Task Raise<T>(T args) where T : INotification
    {
        var mediator = Publisher.Invoke();
        await mediator.Publish(args);
    }
}