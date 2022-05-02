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

		Template:[InsertNewDataModelContextPropertyTextHere]
		
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(t => t.GetProperties())
                                                       .Where(p => p.ClrType == typeof(decimal)
                                                                   || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }    
			
			Template:[InsertNewEFFluentAttributesTextHere]
			Template:[InsertNewEFFluentAttributesUniqueTextHere]
			Template:[InsertNewEFFluentAttributesStringLengthTextHere]			
			
            base.OnModelCreating(modelBuilder);
        }
    }
}
