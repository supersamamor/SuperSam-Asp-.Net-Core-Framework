namespace CTI.DSF.Web;

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
            .Concat(GeneratePermissionsForModule("TaskList"))
			.Concat(GeneratePermissionsForModule("Assignment"))
            .Concat(GeneratePermissionsForModule("Delivery"))
            .Concat(GeneratePermissionsForModule("Company"))
            .Concat(GeneratePermissionsForModule("Department"))
            .Concat(GeneratePermissionsForModule("Section"))
            .Concat(GeneratePermissionsForModule("Team"))
            .Concat(GeneratePermissionsForModule("Holiday"))
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
    public static class TaskList
	{
		public const string View = "Permission.TaskList.View";
		public const string Create = "Permission.TaskList.Create";
		public const string Edit = "Permission.TaskList.Edit";
		public const string Delete = "Permission.TaskList.Delete";
		public const string Approve = "Permission.TaskList.Approve";
	}
	public static class Assignment
	{
		public const string View = "Permission.Assignment.View";
		public const string Create = "Permission.Assignment.Create";
		public const string Edit = "Permission.Assignment.Edit";
		public const string Delete = "Permission.Assignment.Delete";
	}

    public static class Delivery
    {
        public const string View = "Permission.Delivery.View";
        public const string Create = "Permission.Delivery.Create";
        public const string Edit = "Permission.Delivery.Edit";
        public const string Delete = "Permission.Delivery.Delete";
        public const string Approve = "Permission.Delivery.Approve";
    }

    public static class Company
    {
        public const string View = "Permission.Company.View";
        public const string Create = "Permission.Company.Create";
        public const string Edit = "Permission.Company.Edit";
        public const string Delete = "Permission.Company.Delete";
    }
    public static class Department
    {
        public const string View = "Permission.Department.View";
        public const string Create = "Permission.Department.Create";
        public const string Edit = "Permission.Department.Edit";
        public const string Delete = "Permission.Department.Delete";
    }
    public static class Section
    {
        public const string View = "Permission.Section.View";
        public const string Create = "Permission.Section.Create";
        public const string Edit = "Permission.Section.Edit";
        public const string Delete = "Permission.Section.Delete";
    }
    public static class Team
    {
        public const string View = "Permission.Team.View";
        public const string Create = "Permission.Team.Create";
        public const string Edit = "Permission.Team.Edit";
        public const string Delete = "Permission.Team.Delete";
    }

    public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
    public static class Holiday
    {
        public const string View = "Permission.Holiday.View";
        public const string Create = "Permission.Holiday.Create";
        public const string Edit = "Permission.Holiday.Edit";
        public const string Delete = "Permission.Holiday.Delete";
    }
}
