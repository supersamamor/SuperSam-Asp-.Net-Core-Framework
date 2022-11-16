namespace CTI.ELMS.Web;

public static class Permission
{
    public static IEnumerable<string> GenerateAllPermissions()
    {
        return GeneratePermissionsForModule("Admin")
            .Concat(GeneratePermissionsForModule("Entities"))
            .Concat(GeneratePermissionsForModule("Roles"))
            .Concat(GeneratePermissionsForModule("Users"))
            .Concat(GeneratePermissionsForModule("Apis"))
            .Concat(GeneratePermissionsForModule("Applications"))
            .Concat(GeneratePermissionsForModule("AuditTrail"))
            .Concat(GeneratePermissionsForModule("PPlusConnectionSetup"))
            .Concat(GeneratePermissionsForModule("EntityGroup"))
            .Concat(GeneratePermissionsForModule("Project"))
            .Concat(GeneratePermissionsForModule("UserProjectAssignment"))
            .Concat(GeneratePermissionsForModule("Unit"))
            .Concat(GeneratePermissionsForModule("UnitBudget"))
            .Concat(GeneratePermissionsForModule("LeadSource"))
            .Concat(GeneratePermissionsForModule("LeadTouchPoint"))
            .Concat(GeneratePermissionsForModule("LeadTask"))
            .Concat(GeneratePermissionsForModule("NextStep"))
            .Concat(GeneratePermissionsForModule("ClientFeedback"))
            .Concat(GeneratePermissionsForModule("LeadTaskClientFeedBack"))
            .Concat(GeneratePermissionsForModule("LeadTaskNextStep"))
            .Concat(GeneratePermissionsForModule("BusinessNature"))
            .Concat(GeneratePermissionsForModule("BusinessNatureSubItem"))
            .Concat(GeneratePermissionsForModule("BusinessNatureCategory"))
            .Concat(GeneratePermissionsForModule("OperationType"))
            .Concat(GeneratePermissionsForModule("Salutation"))
            .Concat(GeneratePermissionsForModule("Lead"))
            .Concat(GeneratePermissionsForModule("Contact"))
            .Concat(GeneratePermissionsForModule("ContactPerson"))
            .Concat(GeneratePermissionsForModule("Activity"))
            .Concat(GeneratePermissionsForModule("ActivityHistory"))
            .Concat(GeneratePermissionsForModule("UnitActivity"))
            .Concat(GeneratePermissionsForModule("Offering"))
            .Concat(GeneratePermissionsForModule("OfferingHistory"))
            .Concat(GeneratePermissionsForModule("PreSelectedUnit"))
            .Concat(GeneratePermissionsForModule("UnitOffered"))
            .Concat(GeneratePermissionsForModule("UnitOfferedHistory"))
            .Concat(GeneratePermissionsForModule("UnitGroup"))
            .Concat(GeneratePermissionsForModule("AnnualIncrement"))
            .Concat(GeneratePermissionsForModule("AnnualIncrementHistory"))
            .Concat(GeneratePermissionsForModule("IFCATransactionType"))
            .Concat(GeneratePermissionsForModule("IFCATenantInformation"))
            .Concat(GeneratePermissionsForModule("IFCAUnitInformation"))
            .Concat(GeneratePermissionsForModule("IFCAARLedger"))
            .Concat(GeneratePermissionsForModule("IFCAARAllocation"))
            .Concat(GeneratePermissionsForModule("ReportTableCollectionDetail"))
            .Concat(GeneratePermissionsForModule("ReportTableYTDExpirySummary"))
            .Concat(GeneratePermissionsForModule("AwardNotice"))
            .Concat(GeneratePermissionsForModule("LeaseContract"))
            .Concat(GeneratePermissionsForModule("ApproverSetup"));
    }

    public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        var permissions = new List<string>();
        if (module == "AwardNotice" || module == "LeaseContract" || module == "Offering")
        {
            permissions.Add($"Permission.{module}.View");
        }
        else if (module == "Lead" || module == "Activity") //No Delete
        {
            permissions.AddRange(new List<string>()
            {
                $"Permission.{module}.Create",
                $"Permission.{module}.View",
                $"Permission.{module}.Edit",               
            });
        }
        else
        {
            permissions.AddRange(new List<string>()
            {
                $"Permission.{module}.Create",
                $"Permission.{module}.View",
                $"Permission.{module}.Edit",
                $"Permission.{module}.Delete",             
            });
            if (module == "ApproverSetup")
            {
                permissions.Add($"Permission.{module}.PendingApprovals");
            }
        }
        if (module == "Offering")
        {
            permissions.Add($"Permission.{module}.Approve");
        }
        return permissions;
    }

    public static class Admin
    {
        public const string View = "Permission.Admin.View";
        public const string Create = "Permission.Admin.Create";
        public const string Edit = "Permission.Admin.Edit";
        public const string Delete = "Permission.Admin.Delete";
    }

    public static class Entities
    {
        public const string View = "Permission.Entities.View";
        public const string Create = "Permission.Entities.Create";
        public const string Edit = "Permission.Entities.Edit";
        public const string Delete = "Permission.Entities.Delete";
    }

    public static class Roles
    {
        public const string View = "Permission.Roles.View";
        public const string Create = "Permission.Roles.Create";
        public const string Edit = "Permission.Roles.Edit";
        public const string Delete = "Permission.Roles.Delete";
    }

    public static class Users
    {
        public const string View = "Permission.Users.View";
        public const string Create = "Permission.Users.Create";
        public const string Edit = "Permission.Users.Edit";
        public const string Delete = "Permission.Users.Delete";
    }

    public static class Apis
    {
        public const string View = "Permission.Apis.View";
        public const string Create = "Permission.Apis.Create";
        public const string Edit = "Permission.Apis.Edit";
        public const string Delete = "Permission.Apis.Delete";
    }

    public static class Applications
    {
        public const string View = "Permission.Applications.View";
        public const string Create = "Permission.Applications.Create";
        public const string Edit = "Permission.Applications.Edit";
        public const string Delete = "Permission.Applications.Delete";
    }

    public static class AuditTrail
    {
        public const string View = "Permission.AuditTrail.View";
        public const string Create = "Permission.AuditTrail.Create";
        public const string Edit = "Permission.AuditTrail.Edit";
        public const string Delete = "Permission.AuditTrail.Delete";
    }

    public static class PPlusConnectionSetup
    {
        public const string View = "Permission.PPlusConnectionSetup.View";
        public const string Create = "Permission.PPlusConnectionSetup.Create";
        public const string Edit = "Permission.PPlusConnectionSetup.Edit";
        public const string Delete = "Permission.PPlusConnectionSetup.Delete";
    }
    public static class EntityGroup
    {
        public const string View = "Permission.EntityGroup.View";
        public const string Create = "Permission.EntityGroup.Create";
        public const string Edit = "Permission.EntityGroup.Edit";
        public const string Delete = "Permission.EntityGroup.Delete";
    }
    public static class Project
    {
        public const string View = "Permission.Project.View";
        public const string Create = "Permission.Project.Create";
        public const string Edit = "Permission.Project.Edit";
        public const string Delete = "Permission.Project.Delete";
    }
    public static class UserProjectAssignment
    {
        public const string View = "Permission.UserProjectAssignment.View";
        public const string Create = "Permission.UserProjectAssignment.Create";
        public const string Edit = "Permission.UserProjectAssignment.Edit";
        public const string Delete = "Permission.UserProjectAssignment.Delete";
    }
    public static class Unit
    {
        public const string View = "Permission.Unit.View";
        public const string Create = "Permission.Unit.Create";
        public const string Edit = "Permission.Unit.Edit";
        public const string Delete = "Permission.Unit.Delete";
    }
    public static class UnitBudget
    {
        public const string View = "Permission.UnitBudget.View";
        public const string Create = "Permission.UnitBudget.Create";
        public const string Edit = "Permission.UnitBudget.Edit";
        public const string Delete = "Permission.UnitBudget.Delete";
    }
    public static class LeadSource
    {
        public const string View = "Permission.LeadSource.View";
        public const string Create = "Permission.LeadSource.Create";
        public const string Edit = "Permission.LeadSource.Edit";
        public const string Delete = "Permission.LeadSource.Delete";
    }
    public static class LeadTouchPoint
    {
        public const string View = "Permission.LeadTouchPoint.View";
        public const string Create = "Permission.LeadTouchPoint.Create";
        public const string Edit = "Permission.LeadTouchPoint.Edit";
        public const string Delete = "Permission.LeadTouchPoint.Delete";
    }
    public static class LeadTask
    {
        public const string View = "Permission.LeadTask.View";
        public const string Create = "Permission.LeadTask.Create";
        public const string Edit = "Permission.LeadTask.Edit";
        public const string Delete = "Permission.LeadTask.Delete";
    }
    public static class NextStep
    {
        public const string View = "Permission.NextStep.View";
        public const string Create = "Permission.NextStep.Create";
        public const string Edit = "Permission.NextStep.Edit";
        public const string Delete = "Permission.NextStep.Delete";
    }
    public static class ClientFeedback
    {
        public const string View = "Permission.ClientFeedback.View";
        public const string Create = "Permission.ClientFeedback.Create";
        public const string Edit = "Permission.ClientFeedback.Edit";
        public const string Delete = "Permission.ClientFeedback.Delete";
    }
    public static class LeadTaskClientFeedBack
    {
        public const string View = "Permission.LeadTaskClientFeedBack.View";
        public const string Create = "Permission.LeadTaskClientFeedBack.Create";
        public const string Edit = "Permission.LeadTaskClientFeedBack.Edit";
        public const string Delete = "Permission.LeadTaskClientFeedBack.Delete";
    }
    public static class LeadTaskNextStep
    {
        public const string View = "Permission.LeadTaskNextStep.View";
        public const string Create = "Permission.LeadTaskNextStep.Create";
        public const string Edit = "Permission.LeadTaskNextStep.Edit";
        public const string Delete = "Permission.LeadTaskNextStep.Delete";
    }
    public static class BusinessNature
    {
        public const string View = "Permission.BusinessNature.View";
        public const string Create = "Permission.BusinessNature.Create";
        public const string Edit = "Permission.BusinessNature.Edit";
        public const string Delete = "Permission.BusinessNature.Delete";
    }
    public static class BusinessNatureSubItem
    {
        public const string View = "Permission.BusinessNatureSubItem.View";
        public const string Create = "Permission.BusinessNatureSubItem.Create";
        public const string Edit = "Permission.BusinessNatureSubItem.Edit";
        public const string Delete = "Permission.BusinessNatureSubItem.Delete";
    }
    public static class BusinessNatureCategory
    {
        public const string View = "Permission.BusinessNatureCategory.View";
        public const string Create = "Permission.BusinessNatureCategory.Create";
        public const string Edit = "Permission.BusinessNatureCategory.Edit";
        public const string Delete = "Permission.BusinessNatureCategory.Delete";
    }
    public static class OperationType
    {
        public const string View = "Permission.OperationType.View";
        public const string Create = "Permission.OperationType.Create";
        public const string Edit = "Permission.OperationType.Edit";
        public const string Delete = "Permission.OperationType.Delete";
    }
    public static class Salutation
    {
        public const string View = "Permission.Salutation.View";
        public const string Create = "Permission.Salutation.Create";
        public const string Edit = "Permission.Salutation.Edit";
        public const string Delete = "Permission.Salutation.Delete";
    }
    public static class Lead
    {
        public const string View = "Permission.Lead.View";
        public const string Create = "Permission.Lead.Create";
        public const string Edit = "Permission.Lead.Edit";        
    }
    public static class Activity
    {
        public const string View = "Permission.Activity.View";
        public const string Create = "Permission.Activity.Create";
        public const string Edit = "Permission.Activity.Edit";
    }
    public static class Offering
    {
        public const string View = "Permission.Offering.View";
        public const string Create = "Permission.Offering.Create";
        public const string Edit = "Permission.Offering.Edit";   
        public const string Approve = "Permission.Offering.Approve";
    }
    public static class Contact
    {
        public const string View = "Permission.Contact.View";
        public const string Create = "Permission.Contact.Create";
        public const string Edit = "Permission.Contact.Edit";
        public const string Delete = "Permission.Contact.Delete";
    }
    public static class ContactPerson
    {
        public const string View = "Permission.ContactPerson.View";
        public const string Create = "Permission.ContactPerson.Create";
        public const string Edit = "Permission.ContactPerson.Edit";
        public const string Delete = "Permission.ContactPerson.Delete";
    }
  
    public static class ActivityHistory
    {
        public const string View = "Permission.ActivityHistory.View";
        public const string Create = "Permission.ActivityHistory.Create";
        public const string Edit = "Permission.ActivityHistory.Edit";
        public const string Delete = "Permission.ActivityHistory.Delete";
    }
    public static class UnitActivity
    {
        public const string View = "Permission.UnitActivity.View";
        public const string Create = "Permission.UnitActivity.Create";
        public const string Edit = "Permission.UnitActivity.Edit";
        public const string Delete = "Permission.UnitActivity.Delete";
    }

    public static class OfferingHistory
    {
        public const string View = "Permission.OfferingHistory.View";
        public const string Create = "Permission.OfferingHistory.Create";
        public const string Edit = "Permission.OfferingHistory.Edit";
        public const string Delete = "Permission.OfferingHistory.Delete";
    }
    public static class PreSelectedUnit
    {
        public const string View = "Permission.PreSelectedUnit.View";
        public const string Create = "Permission.PreSelectedUnit.Create";
        public const string Edit = "Permission.PreSelectedUnit.Edit";
        public const string Delete = "Permission.PreSelectedUnit.Delete";
    }
    public static class UnitOffered
    {
        public const string View = "Permission.UnitOffered.View";
        public const string Create = "Permission.UnitOffered.Create";
        public const string Edit = "Permission.UnitOffered.Edit";
        public const string Delete = "Permission.UnitOffered.Delete";
    }
    public static class UnitOfferedHistory
    {
        public const string View = "Permission.UnitOfferedHistory.View";
        public const string Create = "Permission.UnitOfferedHistory.Create";
        public const string Edit = "Permission.UnitOfferedHistory.Edit";
        public const string Delete = "Permission.UnitOfferedHistory.Delete";
    }
    public static class UnitGroup
    {
        public const string View = "Permission.UnitGroup.View";
        public const string Create = "Permission.UnitGroup.Create";
        public const string Edit = "Permission.UnitGroup.Edit";
        public const string Delete = "Permission.UnitGroup.Delete";
    }
    public static class AnnualIncrement
    {
        public const string View = "Permission.AnnualIncrement.View";
        public const string Create = "Permission.AnnualIncrement.Create";
        public const string Edit = "Permission.AnnualIncrement.Edit";
        public const string Delete = "Permission.AnnualIncrement.Delete";
    }
    public static class AnnualIncrementHistory
    {
        public const string View = "Permission.AnnualIncrementHistory.View";
        public const string Create = "Permission.AnnualIncrementHistory.Create";
        public const string Edit = "Permission.AnnualIncrementHistory.Edit";
        public const string Delete = "Permission.AnnualIncrementHistory.Delete";
    }
    public static class IFCATransactionType
    {
        public const string View = "Permission.IFCATransactionType.View";
        public const string Create = "Permission.IFCATransactionType.Create";
        public const string Edit = "Permission.IFCATransactionType.Edit";
        public const string Delete = "Permission.IFCATransactionType.Delete";
    }
    public static class IFCATenantInformation
    {
        public const string View = "Permission.IFCATenantInformation.View";
        public const string Create = "Permission.IFCATenantInformation.Create";
        public const string Edit = "Permission.IFCATenantInformation.Edit";
        public const string Delete = "Permission.IFCATenantInformation.Delete";
    }
    public static class IFCAUnitInformation
    {
        public const string View = "Permission.IFCAUnitInformation.View";
        public const string Create = "Permission.IFCAUnitInformation.Create";
        public const string Edit = "Permission.IFCAUnitInformation.Edit";
        public const string Delete = "Permission.IFCAUnitInformation.Delete";
    }
    public static class IFCAARLedger
    {
        public const string View = "Permission.IFCAARLedger.View";
        public const string Create = "Permission.IFCAARLedger.Create";
        public const string Edit = "Permission.IFCAARLedger.Edit";
        public const string Delete = "Permission.IFCAARLedger.Delete";
    }
    public static class IFCAARAllocation
    {
        public const string View = "Permission.IFCAARAllocation.View";
        public const string Create = "Permission.IFCAARAllocation.Create";
        public const string Edit = "Permission.IFCAARAllocation.Edit";
        public const string Delete = "Permission.IFCAARAllocation.Delete";
    }
    public static class ReportTableCollectionDetail
    {
        public const string View = "Permission.ReportTableCollectionDetail.View";
        public const string Create = "Permission.ReportTableCollectionDetail.Create";
        public const string Edit = "Permission.ReportTableCollectionDetail.Edit";
        public const string Delete = "Permission.ReportTableCollectionDetail.Delete";
    }
    public static class ReportTableYTDExpirySummary
    {
        public const string View = "Permission.ReportTableYTDExpirySummary.View";
        public const string Create = "Permission.ReportTableYTDExpirySummary.Create";
        public const string Edit = "Permission.ReportTableYTDExpirySummary.Edit";
        public const string Delete = "Permission.ReportTableYTDExpirySummary.Delete";
    }

    public static class ApproverSetup
    {
        public const string Create = "Permission.ApproverSetup.Create";
        public const string View = "Permission.ApproverSetup.View";
        public const string Edit = "Permission.ApproverSetup.Edit";
        public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
    }
    public static class AwardNotice
    {
        public const string View = "Permission.AwardNotice.View";  
    }
    public static class LeaseContract
    {
        public const string View = "Permission.LeaseContract.View";
    }
}
