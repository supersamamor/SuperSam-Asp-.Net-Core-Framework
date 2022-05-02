using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity;
using System.Linq;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data
{
    public class ApplicationContext : AuditableDbContext
    {
        private readonly AuthenticatedUser _authenticatedUser;

        public ApplicationContext(DbContextOptions<ApplicationContext> options,
                                  AuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<MainModulePlaceHolder> MainModulePlaceHolder { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(t => t.GetProperties())
                                                       .Where(p => p.ClrType == typeof(decimal)
                                                                   || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }   
            modelBuilder.Entity<MainModulePlaceHolder>().HasIndex(e => e.LastModifiedDate);
            modelBuilder.Entity<MainModulePlaceHolder>().HasIndex(e => e.Entity);
            modelBuilder.Entity<MainModulePlaceHolder>().HasQueryFilter(e => e.Entity == _authenticatedUser.Entity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
