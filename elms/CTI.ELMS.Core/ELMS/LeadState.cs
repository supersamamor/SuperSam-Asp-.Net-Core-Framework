using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record LeadState : BaseEntity
{
    public string ClientType { get; init; } = "";
    public string Brand { get; init; } = "";
    public string Company { get; init; } = "";
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? Province { get; init; }
    public string Country { get; init; } = "";
    public string LeadSourceId { get; init; } = "";
    public string LeadTouchpointId { get; init; } = "";
    public string OperationTypeID { get; init; } = "";
    public string BusinessNatureID { get; init; } = "";
    public string BusinessNatureSubItemID { get; init; } = "";
    public string? BusinessNatureCategoryID { get; init; }
    public string? TINNumber { get; init; }
    public bool IsFranchise { get; init; }
    public DateTime LatestUpdatedDate { get; set; } = DateTime.UtcNow;
    public string LatestUpdatedByUsername { get; init; } = "";    
    public LeadSourceState? LeadSource { get; init; }
    public LeadTouchPointState? LeadTouchPoint { get; init; }
    public OperationTypeState? OperationType { get; init; }
    public BusinessNatureState? BusinessNature { get; init; }
    public BusinessNatureSubItemState? BusinessNatureSubItem { get; init; }
    public BusinessNatureCategoryState? BusinessNatureCategory { get; init; }

    public IList<ContactState>? ContactList { get; set; }
    public IList<ContactPersonState>? ContactPersonList { get; set; }
    public IList<ActivityState>? ActivityList { get; set; }
    public IList<OfferingState>? OfferingList { get; set; }
    public IList<OfferingHistoryState>? OfferingHistoryList { get; set; }
    #region Derived Fields
    public string Email
    {
        get
        {
            return "";
        }
    }
    public string ContactNumber
    {
        get
        {
            return "";
        }
    }
    public string DisplayName
    {
        get
        {
            string retName = (this.Brand != null ? this.Brand + " " : "") + (this.Company != null ? "(" + this.Company + ")" : "");
            return retName.ToUpper();
        }
    }
    public int LeadAging
    {
        get { return (int)(DateTime.UtcNow - this.CreatedDate).TotalDays; }
    }
    #endregion
}
