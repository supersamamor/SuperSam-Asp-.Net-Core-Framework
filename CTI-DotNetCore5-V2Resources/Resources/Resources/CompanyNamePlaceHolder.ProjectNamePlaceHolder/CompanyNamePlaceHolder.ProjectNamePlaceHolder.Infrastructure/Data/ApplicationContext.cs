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

        Template:[InsertNewDataModelContextPropertyTextHere]

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetBaseEntityFields(_authenticatedUser);
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Template:[InsertNewEFFluentAttributesTextHere]
			Template:[InsertNewEFFluentAttributesUniqueTextHere]
			Template:[InsertNewEFFluentAttributesStringLengthTextHere]
            base.OnModelCreating(modelBuilder);
        }
    }
}
