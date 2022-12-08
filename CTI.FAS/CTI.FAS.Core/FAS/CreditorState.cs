using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record CreditorState : BaseEntity
{
    public string CreditorAccount { get; init; } = "";
    public string PayeeAccountName { get; init; } = "";
    public string? PayeeAccountLongDescription { get; init; }
    public string? PayeeAccountCode { get; init; }
    public string? PayeeAccountTIN { get; init; }
    public string PayeeAccountAddress { get; init; } = "";
    public string Email { get; init; } = "";
    public string? DeliveryOptions { get; init; } = Constants.DeliveryOptions.Delivery;
    public string DatabaseConnectionSetupId { get; init; } = "";

    public DatabaseConnectionSetupState? DatabaseConnectionSetup { get; init; }

    public IList<EnrolledPayeeState>? EnrolledPayeeList { get; set; }
    public string? CreditorDisplayDescription
    {
        get
        {
            return this.CreditorAccount + " - " + this.PayeeAccountName;
        }
    }

}
