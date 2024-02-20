using CTI.ContractManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Web.Areas.Identity.Data;

public static class DefaultEntity
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        using var context = new IdentityContext(
            serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());
        var entity = await context.Entities.FirstOrDefaultAsync(e => e.Name == "Default");
        if (entity == null)
        {
            context.Entities.Add(new("DEFAULT", "Default"));
            await context.SaveChangesAsync();
        }
    }
}
