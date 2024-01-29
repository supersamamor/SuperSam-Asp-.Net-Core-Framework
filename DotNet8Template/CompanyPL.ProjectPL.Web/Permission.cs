namespace CompanyPL.ProjectPL.Web;

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
			.Concat(GeneratePermissionsForModule("Report"))
            .Concat(GeneratePermissionsForModule("ReportSetup"))
            .Concat(GeneratePermissionsWithUploadForModule("Employee"))
			.Concat(GeneratePermissionsWithUploadForModule("ContactInformation"))
			.Concat(GeneratePermissionsWithUploadForModule("HealthDeclaration"))
			
			.Concat(GeneratepPendingApprovalPermissionsForModule("ApproverSetup"));
    }

	public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        var permissions = new List<string>()
        {
            $"Permission.{module}.Create",
            $"Permission.{module}.View",
            $"Permission.{module}.Edit",
            $"Permission.{module}.Delete",
        };
		
		permissions.Add($"Permission.{module}.Approve");
        return permissions;
    }
    public static IEnumerable<string> GenerateApprovalPermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permission.{module}.Approve",
        };
    }
    public static IEnumerable<string> GeneratepPendingApprovalPermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permission.{module}.Create",
            $"Permission.{module}.View",
            $"Permission.{module}.Edit",
            $"Permission.{module}.Delete",
            $"Permission.{module}.PendingApprovals"
        };
    }

    public static IEnumerable<string> GeneratePermissionsWithUploadForModule(string module)
    {
        var permissions = GeneratePermissionsForModule(module);
        return permissions.Concat(new List<string>() { $"Permission.{module}.Upload" });        
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
	public static class Report
    {
        public const string View = "Permission.Report.View";
    }
    public static class ReportSetup
    {
        public const string View = "Permission.ReportSetup.View";
        public const string Create = "Permission.ReportSetup.Create";
        public const string Edit = "Permission.ReportSetup.Edit";
        public const string Delete = "Permission.ReportSetup.Delete";
        public const string Approve = "Permission.ReportSetup.Approve";
    }
    public static class Employee
	{
		public const string View = "Permission.Employee.View";
		public const string Create = "Permission.Employee.Create";
		public const string Edit = "Permission.Employee.Edit";
		public const string Delete = "Permission.Employee.Delete";
		public const string Upload = "Permission.Employee.Upload";
		public const string Approve = "Permission.Employee.Approve";
	}
	public static class ContactInformation
	{
		public const string View = "Permission.ContactInformation.View";
		public const string Create = "Permission.ContactInformation.Create";
		public const string Edit = "Permission.ContactInformation.Edit";
		public const string Delete = "Permission.ContactInformation.Delete";
		public const string Upload = "Permission.ContactInformation.Upload";
	}
	public static class HealthDeclaration
	{
		public const string View = "Permission.HealthDeclaration.View";
		public const string Create = "Permission.HealthDeclaration.Create";
		public const string Edit = "Permission.HealthDeclaration.Edit";
		public const string Delete = "Permission.HealthDeclaration.Delete";
		public const string Upload = "Permission.HealthDeclaration.Upload";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
