using Microsoft.AspNetCore.Identity;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public Entity? Entity { get; set; }
    public string? EntityId { get; set; }

    public bool IsActive { get; set; } = false;
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastUnsucccessfulLogin { get; set; }
}

public record Entity(string Id, string Name)
{
    public IEnumerable<ApplicationUser>? Users { get; set; }
}
public class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
        this.Id = Guid.NewGuid().ToString();
    }
}