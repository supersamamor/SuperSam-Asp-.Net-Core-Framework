using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.SecurityData.Models;
namespace Template.SecurityData
{
    public class TemplateContext : IdentityDbContext<AppUser>
    {
        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }  
    }
}
