using Cti.Core.Domain.Common;
using Cti.Core.Domain.Common.Contracts;

namespace ProjectNamePlaceHolder.Services.Domain.Common.Events
{ 
    public static class EntityDeletedEvent
    {
        public static EntityDeletedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
            where TEntity : IAggregateRoot
            => new EntityDeletedEvent<TEntity>(entity);
    }

    public class EntityDeletedEvent<TEntity> : DomainEvent
        where TEntity : IAggregateRoot
    {
        internal EntityDeletedEvent(TEntity entity) => Entity = entity;

        public TEntity Entity { get; }
    }
}