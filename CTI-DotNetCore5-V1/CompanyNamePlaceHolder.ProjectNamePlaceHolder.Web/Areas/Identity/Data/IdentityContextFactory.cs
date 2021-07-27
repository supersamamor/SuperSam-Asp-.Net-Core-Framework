using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data
{
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
}


