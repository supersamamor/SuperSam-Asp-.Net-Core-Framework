using Cti.Core.Domain.Common;
using Cti.Core.Domain.Common.Contracts;

namespace CHANGE_TO_APP_NAME.Services.Domain.Common.Events
{ 
    public static class EntityCreatedEvent
    {
        public static EntityCreatedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
            => new EntityCreatedEvent<TEntity>(entity);
    }

    public class EntityCreatedEvent<TEntity> : DomainEvent
        where TEntity : IAggregateRoot
    {
        internal EntityCreatedEvent(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }
    }

}
