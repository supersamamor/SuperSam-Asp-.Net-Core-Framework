using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data
{
    public static class DefaultEntity
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            using var context = new IdentityContext(
                serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());
            var entity = await context.Entities.FirstOrDefaultAsync(e => e.Name == "Default");
            if (entity == null)
            {
                context.Entities.Add(new(Guid.NewGuid().ToString(), "Default"));
                await context.SaveChangesAsync();
            }
        }
    }
}
