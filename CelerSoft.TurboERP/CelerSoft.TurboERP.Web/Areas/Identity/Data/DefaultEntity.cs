using CelerSoft.TurboERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Web.Areas.Identity.Data;

public static class DefaultEntity
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new IdentityContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());
        var entity = await context.Entities.FirstOrDefaultAsync(e => e.Name == Core.Constants.Entities.Default);
        if (entity == null)
        {
            context.Entities.Add(new( Core.Constants.Entities.Default.ToUpper(),  Core.Constants.Entities.Default));
            await context.SaveChangesAsync();
        }
    }
}
