using Cti.Core.Domain.Common;
using Cti.Core.Domain.Common.Contracts;

namespace ProjectNamePlaceHolder.Services.Domain.Common.Events
{ 
    public static class EntityUpdatedEvent
    {
        public static EntityUpdatedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
            => new EntityUpdatedEvent<TEntity>(entity);
    }

    public class EntityUpdatedEvent<TEntity> : DomainEvent
        where TEntity : IAggregateRoot
    {
        internal EntityUpdatedEvent(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }
    }

}