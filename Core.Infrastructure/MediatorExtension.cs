using Core.Domain.SeedWork;
using Core.Infrastructure.Context;
using MediatR;

namespace Core.Infrastructure;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, WarehouseContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity is { DomainEvents: not null } && x.Entity.DomainEvents.Any());

        var entityEntries = domainEntities.ToList();
        var domainEvents = entityEntries
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        entityEntries.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}