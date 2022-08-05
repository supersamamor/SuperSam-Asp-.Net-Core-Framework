namespace CTI.TenantSales.Web;

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
            .Concat(GeneratePermissionsForModule("DatabaseConnectionSetup"))
            .Concat(GeneratePermissionsForModule("BusinessUnit"))
            .Concat(GeneratePermissionsForModule("RentalType"))
            .Concat(GeneratePermissionsForModule("Company"))
            .Concat(GeneratePermissionsForModule("Project"))
            .Concat(GeneratePermissionsForModule("Theme"))
            .Concat(GeneratePermissionsForModule("Tenant"))
            .Concat(GeneratePermissionsForModule("TenantPOSSales"))
            .Concat(GeneratePermissionsForModule("Classification"))
            .Concat(GeneratePermissionsForModule("Category"))
            .Concat(GeneratePermissionsForModule("ProjectBusinessUnit"))
            .Concat(GeneratePermissionsForModule("TenantLot"))
            .Concat(GeneratePermissionsForModule("Level"))
            .Concat(GeneratePermissionsForModule("SalesCategory"))
            .Concat(GeneratePermissionsForModule("TenantContact"))
            .Concat(GeneratePermissionsForModule("TenantPOS"))
            .Concat(GeneratePermissionsForModule("Revalidate"))
            .Concat(GeneratePermissionsForModule("Reports"))
            .Concat(GeneratePermissionsForModule("MasterFile"))
            .Concat(GeneratePermissionsForModule("Sales"))
            .Concat(GeneratePermissionsForModule("ApproverSetup"));
    }
    public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        var permissions = new List<string>()
        {
            $"Permission.{module}.Create",
            $"Permission.{module}.View",
            $"Permission.{module}.Edit",
            $"Permission.{module}.Delete",
            $"Permission.{module}.Approve",
        };
        if (module == "ApproverSetup")
        {
            permissions.Add($"Permission.{module}.PendingApprovals");
        }
        if (module == "Reports")
        {
            permissions.Add($"Permission.{module}.DailySales");
            permissions.Add($"Permission.{module}.SalesGrowth");
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

    public static class DatabaseConnectionSetup
    {
        public const string View = "Permission.DatabaseConnectionSetup.View";
        public const string Create = "Permission.DatabaseConnectionSetup.Create";
        public const string Edit = "Permission.DatabaseConnectionSetup.Edit";
        public const string Delete = "Permission.DatabaseConnectionSetup.Delete";
        public const string Approve = "Permission.DatabaseConnectionSetup.Approve";
    }
    public static class BusinessUnit
    {
        public const string View = "Permission.BusinessUnit.View";
        public const string Create = "Permission.BusinessUnit.Create";
        public const string Edit = "Permission.BusinessUnit.Edit";
        public const string Delete = "Permission.BusinessUnit.Delete";
    }
    public static class RentalType
    {
        public const string View = "Permission.RentalType.View";
        public const string Create = "Permission.RentalType.Create";
        public const string Edit = "Permission.RentalType.Edit";
        public const string Delete = "Permission.RentalType.Delete";
    }
    public static class Company
    {
        public const string View = "Permission.Company.View";
        public const string Create = "Permission.Company.Create";
        public const string Edit = "Permission.Company.Edit";
        public const string Delete = "Permission.Company.Delete";
    }
    public static class Project
    {
        public const string View = "Permission.Project.View";
        public const string Create = "Permission.Project.Create";
        public const string Edit = "Permission.Project.Edit";
        public const string Delete = "Permission.Project.Delete";
    }
    public static class Theme
    {
        public const string View = "Permission.Theme.View";
        public const string Create = "Permission.Theme.Create";
        public const string Edit = "Permission.Theme.Edit";
        public const string Delete = "Permission.Theme.Delete";
    }
    public static class Tenant
    {
        public const string View = "Permission.Tenant.View";
        public const string Create = "Permission.Tenant.Create";
        public const string Edit = "Permission.Tenant.Edit";
        public const string Delete = "Permission.Tenant.Delete";
    }
    public static class TenantPOSSales
    {
        public const string View = "Permission.TenantPOSSales.View";   
        public const string Edit = "Permission.TenantPOSSales.Edit";    
    }
    public static class Classification
    {
        public const string View = "Permission.Classification.View";
        public const string Create = "Permission.Classification.Create";
        public const string Edit = "Permission.Classification.Edit";
        public const string Delete = "Permission.Classification.Delete";
    }
    public static class Category
    {
        public const string View = "Permission.Category.View";
        public const string Create = "Permission.Category.Create";
        public const string Edit = "Permission.Category.Edit";
        public const string Delete = "Permission.Category.Delete";
    }
    public static class ProjectBusinessUnit
    {
        public const string View = "Permission.ProjectBusinessUnit.View";
        public const string Create = "Permission.ProjectBusinessUnit.Create";
        public const string Edit = "Permission.ProjectBusinessUnit.Edit";
        public const string Delete = "Permission.ProjectBusinessUnit.Delete";
    }
    public static class TenantLot
    {
        public const string View = "Permission.TenantLot.View";
        public const string Create = "Permission.TenantLot.Create";
        public const string Edit = "Permission.TenantLot.Edit";
        public const string Delete = "Permission.TenantLot.Delete";
    }
    public static class Level
    {
        public const string View = "Permission.Level.View";
        public const string Create = "Permission.Level.Create";
        public const string Edit = "Permission.Level.Edit";
        public const string Delete = "Permission.Level.Delete";
    }
    public static class SalesCategory
    {
        public const string View = "Permission.SalesCategory.View";
        public const string Create = "Permission.SalesCategory.Create";
        public const string Edit = "Permission.SalesCategory.Edit";
        public const string Delete = "Permission.SalesCategory.Delete";
    }
    public static class TenantContact
    {
        public const string View = "Permission.TenantContact.View";
        public const string Create = "Permission.TenantContact.Create";
        public const string Edit = "Permission.TenantContact.Edit";
        public const string Delete = "Permission.TenantContact.Delete";
    }
    public static class TenantPOS
    {
        public const string View = "Permission.TenantPOS.View";
        public const string Create = "Permission.TenantPOS.Create";
        public const string Edit = "Permission.TenantPOS.Edit";
        public const string Delete = "Permission.TenantPOS.Delete";
    }
    public static class Revalidate
    {
        public const string View = "Permission.Revalidate.View";
        public const string Create = "Permission.Revalidate.Create";
        public const string Edit = "Permission.Revalidate.Edit";
        public const string Delete = "Permission.Revalidate.Delete";
    }

    public static class ApproverSetup
    {
        public const string Create = "Permission.ApproverSetup.Create";
        public const string View = "Permission.ApproverSetup.View";
        public const string Edit = "Permission.ApproverSetup.Edit";
        public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
    }
    public static class Reports
    {

        public const string View = "Permission.Reports.View";
        public const string DailySales = "Permission.Reports.DailySales";
        public const string SalesGrowth = "Permission.Reports.SalesGrowth";
    }
    public static class MasterFile
    {
        public const string View = "Permission.MasterFile.View";
    }
    public static class Sales
    {
        public const string View = "Permission.Sales.View";
    }
}
