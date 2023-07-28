namespace CTI.DPI.Web;

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
			.Concat(GeneratePermissionsForModule("ReportTable"))
			.Concat(GeneratePermissionsForModule("ReportTableJoinParameter"))
			.Concat(GeneratePermissionsForModule("ReportColumnHeader"))
			.Concat(GeneratePermissionsForModule("ReportColumnDetail"))
			.Concat(GeneratePermissionsForModule("ReportFilterGrouping"))
			.Concat(GeneratePermissionsForModule("ReportColumnFilter"))
			.Concat(GeneratePermissionsForModule("ReportQueryFilter"))
			
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
	public static class ReportTable
	{
		public const string View = "Permission.ReportTable.View";
		public const string Create = "Permission.ReportTable.Create";
		public const string Edit = "Permission.ReportTable.Edit";
		public const string Delete = "Permission.ReportTable.Delete";
	}
	public static class ReportTableJoinParameter
	{
		public const string View = "Permission.ReportTableJoinParameter.View";
		public const string Create = "Permission.ReportTableJoinParameter.Create";
		public const string Edit = "Permission.ReportTableJoinParameter.Edit";
		public const string Delete = "Permission.ReportTableJoinParameter.Delete";
	}
	public static class ReportColumnHeader
	{
		public const string View = "Permission.ReportColumnHeader.View";
		public const string Create = "Permission.ReportColumnHeader.Create";
		public const string Edit = "Permission.ReportColumnHeader.Edit";
		public const string Delete = "Permission.ReportColumnHeader.Delete";
	}
	public static class ReportColumnDetail
	{
		public const string View = "Permission.ReportColumnDetail.View";
		public const string Create = "Permission.ReportColumnDetail.Create";
		public const string Edit = "Permission.ReportColumnDetail.Edit";
		public const string Delete = "Permission.ReportColumnDetail.Delete";
	}
	public static class ReportFilterGrouping
	{
		public const string View = "Permission.ReportFilterGrouping.View";
		public const string Create = "Permission.ReportFilterGrouping.Create";
		public const string Edit = "Permission.ReportFilterGrouping.Edit";
		public const string Delete = "Permission.ReportFilterGrouping.Delete";
	}
	public static class ReportColumnFilter
	{
		public const string View = "Permission.ReportColumnFilter.View";
		public const string Create = "Permission.ReportColumnFilter.Create";
		public const string Edit = "Permission.ReportColumnFilter.Edit";
		public const string Delete = "Permission.ReportColumnFilter.Delete";
	}
	public static class ReportQueryFilter
	{
		public const string View = "Permission.ReportQueryFilter.View";
		public const string Create = "Permission.ReportQueryFilter.Create";
		public const string Edit = "Permission.ReportQueryFilter.Edit";
		public const string Delete = "Permission.ReportQueryFilter.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
