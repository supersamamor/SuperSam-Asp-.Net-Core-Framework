using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;

public record ProjectState : BaseEntity
{
    public string Code { get; init; } = "";
    public string Status { get; init; } = "Active";
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
}

public enum ProjectStatuses
{
    Active,
    Inactive,
    [Description("Under Construction")]
    UnderConstruction
}