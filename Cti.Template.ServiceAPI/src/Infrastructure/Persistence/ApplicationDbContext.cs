using Cti.Core.Application.Common.Interfaces;
using Cti.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CHANGE_TO_APP_NAME.Services.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventService domainEventService, 
            IOptionsMonitor<Infrastructure.Configurations.DbContextOptions> dbOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;

            var _dbOptions = dbOptions.CurrentValue;
            if (_dbOptions.UseIsolationLevelReadUncommitted)
            {
                Database.OpenConnection();
                Database.ExecuteSqlRaw("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            HandleAudit();
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents();
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private void HandleAudit()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = _dateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService.Username ?? _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateModified = _dateTime.Now;
                        entry.Entity.ModifiedBy = _currentUserService.Username ?? _currentUserService.UserId;
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDeletableEntity softDelete)
                        {
                            softDelete.IsDeleted = true;
                            softDelete.DateDeleted = _dateTime.Now;
                            softDelete.DeletedBy = _currentUserService.Username ?? _currentUserService.UserId;
                            entry.State = EntityState.Modified;
                        }

                        break;
                }
            }
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}

