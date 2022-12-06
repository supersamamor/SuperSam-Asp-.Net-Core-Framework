using CTI.Common.Core.Base.Models;
namespace CTI.FAS.Core.FAS;

public record CompanyState : BaseEntity
{
    public string DatabaseConnectionSetupId { get; init; } = "";
    public string Name { get; init; } = "";
    public string Code { get; init; } = "";
    public string? EntityAddress { get; init; }
    public string? EntityAddressSecondLine { get; init; }
    public string? EntityDescription { get; init; }
    public string? EntityShortName { get; init; }
    public bool IsDisabled { get; init; }
    public string? TINNo { get; init; }
    public string? SubmitPlace { get; init; }
    public decimal? SubmitDeadline { get; init; }
    public string? EmailTelephoneNumber { get; init; }
    public string? ImageLogo { get; init; }
    public string? BankName { get; init; }
    public string? BankCode { get; init; }
    public string? AccountName { get; init; }
    public string? AccountType { get; init; }
    public string? AccountNumber { get; init; }
    public DatabaseConnectionSetupState? DatabaseConnectionSetup { get; init; }
    public IList<ProjectState>? ProjectList { get; set; }
    public IList<UserEntityState>? UserEntityList { get; set; }
    public IList<EnrolledPayeeState>? EnrolledPayeeList { get; set; }
    public string? EntityDisplayDescription
    {
        get
        {
            return this.DatabaseConnectionSetup?.Name + " - " + this.Code + " - " + this.Name;
        }
    }
}
