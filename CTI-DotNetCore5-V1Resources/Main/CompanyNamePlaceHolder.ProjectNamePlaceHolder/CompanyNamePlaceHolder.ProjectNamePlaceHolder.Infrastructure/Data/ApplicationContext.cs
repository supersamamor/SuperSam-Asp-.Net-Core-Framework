using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationContext(DbContextOptions<ApplicationContext> options,
                                  IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<MainModulePlaceHolder> MainModulePlaceHolder { get; set; } = default!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetBaseEntityFields(_authenticatedUser);
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainModulePlaceHolder>().HasIndex(p => p.Entity);
            modelBuilder.Entity<MainModulePlaceHolder>().HasIndex(p => p.LastModifiedDate);
            modelBuilder.Entity<MainModulePlaceHolder>().HasQueryFilter(p => EF.Property<string>(p, "Entity") == _authenticatedUser.Entity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
