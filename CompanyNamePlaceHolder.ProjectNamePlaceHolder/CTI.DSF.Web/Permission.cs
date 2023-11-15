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
            .Concat(GeneratePermissionsForModule("Company"))
			.Concat(GeneratePermissionsForModule("Department"))
			.Concat(GeneratePermissionsForModule("Section"))
			.Concat(GeneratePermissionsForModule("Team"))
			.Concat(GeneratePermissionsForModule("Holiday"))
			.Concat(GeneratePermissionsForModule("Tags"))
			.Concat(GeneratePermissionsForModule("TaskMaster"))
			.Concat(GeneratePermissionsForModule("TaskCompanyAssignment"))
			.Concat(GeneratePermissionsForModule("TaskApprover"))
			.Concat(GeneratePermissionsForModule("TaskTag"))
			.Concat(GeneratePermissionsForModule("Assignment"))
			.Concat(GeneratePermissionsForModule("Delivery"))
			.Concat(GeneratePermissionsForModule("DeliveryApprovalHistory"))
			
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
			$"Permission.{module}.Upload",
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
    public static class Company
	{
		public const string View = "Permission.Company.View";
		public const string Create = "Permission.Company.Create";
		public const string Edit = "Permission.Company.Edit";
		public const string Delete = "Permission.Company.Delete";
		public const string Upload = "Permission.Company.Upload";
	}
	public static class Department
	{
		public const string View = "Permission.Department.View";
		public const string Create = "Permission.Department.Create";
		public const string Edit = "Permission.Department.Edit";
		public const string Delete = "Permission.Department.Delete";
		public const string Upload = "Permission.Department.Upload";
	}
	public static class Section
	{
		public const string View = "Permission.Section.View";
		public const string Create = "Permission.Section.Create";
		public const string Edit = "Permission.Section.Edit";
		public const string Delete = "Permission.Section.Delete";
		public const string Upload = "Permission.Section.Upload";
	}
	public static class Team
	{
		public const string View = "Permission.Team.View";
		public const string Create = "Permission.Team.Create";
		public const string Edit = "Permission.Team.Edit";
		public const string Delete = "Permission.Team.Delete";
		public const string Upload = "Permission.Team.Upload";
	}
	public static class Holiday
	{
		public const string View = "Permission.Holiday.View";
		public const string Create = "Permission.Holiday.Create";
		public const string Edit = "Permission.Holiday.Edit";
		public const string Delete = "Permission.Holiday.Delete";
		public const string Upload = "Permission.Holiday.Upload";
	}
	public static class Tags
	{
		public const string View = "Permission.Tags.View";
		public const string Create = "Permission.Tags.Create";
		public const string Edit = "Permission.Tags.Edit";
		public const string Delete = "Permission.Tags.Delete";
		public const string Upload = "Permission.Tags.Upload";
	}
	public static class TaskMaster
	{
		public const string View = "Permission.TaskMaster.View";
		public const string Create = "Permission.TaskMaster.Create";
		public const string Edit = "Permission.TaskMaster.Edit";
		public const string Delete = "Permission.TaskMaster.Delete";
		public const string Upload = "Permission.TaskMaster.Upload";
	}
	public static class TaskCompanyAssignment
	{
		public const string View = "Permission.TaskCompanyAssignment.View";
		public const string Create = "Permission.TaskCompanyAssignment.Create";
		public const string Edit = "Permission.TaskCompanyAssignment.Edit";
		public const string Delete = "Permission.TaskCompanyAssignment.Delete";
		public const string Upload = "Permission.TaskCompanyAssignment.Upload";
	}
	public static class TaskApprover
	{
		public const string View = "Permission.TaskApprover.View";
		public const string Create = "Permission.TaskApprover.Create";
		public const string Edit = "Permission.TaskApprover.Edit";
		public const string Delete = "Permission.TaskApprover.Delete";
		public const string Upload = "Permission.TaskApprover.Upload";
	}
	public static class TaskTag
	{
		public const string View = "Permission.TaskTag.View";
		public const string Create = "Permission.TaskTag.Create";
		public const string Edit = "Permission.TaskTag.Edit";
		public const string Delete = "Permission.TaskTag.Delete";
		public const string Upload = "Permission.TaskTag.Upload";
	}
	public static class Assignment
	{
		public const string View = "Permission.Assignment.View";
		public const string Create = "Permission.Assignment.Create";
		public const string Edit = "Permission.Assignment.Edit";
		public const string Delete = "Permission.Assignment.Delete";
		public const string Upload = "Permission.Assignment.Upload";
	}
	public static class Delivery
	{
		public const string View = "Permission.Delivery.View";
		public const string Create = "Permission.Delivery.Create";
		public const string Edit = "Permission.Delivery.Edit";
		public const string Delete = "Permission.Delivery.Delete";
		public const string Upload = "Permission.Delivery.Upload";
		public const string Approve = "Permission.Delivery.Approve";
	}
	public static class DeliveryApprovalHistory
	{
		public const string View = "Permission.DeliveryApprovalHistory.View";
		public const string Create = "Permission.DeliveryApprovalHistory.Create";
		public const string Edit = "Permission.DeliveryApprovalHistory.Edit";
		public const string Delete = "Permission.DeliveryApprovalHistory.Delete";
		public const string Upload = "Permission.DeliveryApprovalHistory.Upload";
		public const string Approve = "Permission.DeliveryApprovalHistory.Approve";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
