using Microsoft.AspNetCore.Identity;

namespace CTI.FAS.Core.Identity;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public DateTime? BirthDate { get; set; }

    [PersonalData]
    public Entity? Entity { get; set; }
    public string? EntityId { get; set; }
    [PersonalData]
    public Group? Group { get; set; }
    public string? GroupId { get; set; }
    public bool IsActive { get; set; } = false;
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastUnsucccessfulLogin { get; set; }
    public string? PplusId { get; set; }
}

public record Entity(string Id, string Name)
{
    public IEnumerable<ApplicationUser>? Users { get; set; }
}
public record Group(string Id, string Name, string Location, string ContactDetails)
{
    public IEnumerable<ApplicationUser>? Users { get; set; }
}