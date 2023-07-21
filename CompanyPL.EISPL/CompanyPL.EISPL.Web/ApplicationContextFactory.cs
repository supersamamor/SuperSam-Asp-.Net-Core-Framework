using CompanyPL.Common.Web.Utility.Identity;
using CompanyPL.EISPL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyPL.EISPL.Web;

class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseSqlServer(configuration.GetConnectionString("ApplicationContext"));
        return new ApplicationContext(builder.Options, new DefaultAuthenticatedUser(new HttpContextAccessor()));
    }
}
