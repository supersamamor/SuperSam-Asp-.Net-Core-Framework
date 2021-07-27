using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<Project> Projects { get; set; } = default!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetBaseEntityFields(_authenticatedUser);
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasIndex(p => p.Entity);
            modelBuilder.Entity<Project>().HasIndex(p => p.LastModifiedDate);
            modelBuilder.Entity<Project>().HasQueryFilter(p => EF.Property<string>(p, "Entity") == _authenticatedUser.Entity);

            base.OnModelCreating(modelBuilder);
        }
    }
}
