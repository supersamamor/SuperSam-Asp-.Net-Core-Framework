using CTI.Common.Core.Base.Models;
namespace CTI.TenantSales.Core.TenantSales;
public record TenantContactState : BaseEntity
{
    public int Group { get; init; }
    public int Type { get; init; }
    public string? Detail { get; init; }
    public string TenantId { get; init; } = "";
    public TenantState? Tenant { get; init; }
}
public enum ContactGroup
{
    Branch = 1,
    ITSupport = 2,
    HeadOffice = 3
}
public enum ContactTypeEnum
{
    Number = 1,
    Email = 2
}