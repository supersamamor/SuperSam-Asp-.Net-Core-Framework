using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record CompanyState : BaseEntity
{
    public string Name { get; init; } = "";
    public string Code { get; init; } = "";
    public string? EntityAddress { get; init; }
    public string? EntityAddressSecondLine { get; init; }
    public string? EntityDescription { get; init; }
    public string? EntityShortName { get; init; }
    public bool IsDisabled { get; init; }
    public string? TINNo { get; init; }
    public string DatabaseConnectionSetupId { get; init; } = "";

    public DatabaseConnectionSetupState? DatabaseConnectionSetup { get; init; }

    public IList<ProjectState>? ProjectList { get; set; }
    public string? DisplayDescription
    {
        get
        {
            return this?.DatabaseConnectionSetup?.Code + "/" + this.Code + " - " + this.Name;
        }
    }

}
