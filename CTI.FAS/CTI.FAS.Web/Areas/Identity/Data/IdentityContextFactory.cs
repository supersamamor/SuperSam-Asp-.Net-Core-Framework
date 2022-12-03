using CTI.FAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CTI.FAS.Web.Areas.Identity.Data;

class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<IdentityContext>();
        builder.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
        return new IdentityContext(builder.Options);
    }
}
