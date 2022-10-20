namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web;

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
            .Concat(GeneratePermissionsForModule("MainModule"))
			.Concat(GeneratePermissionsForModule("ParentModule"))
			.Concat(GeneratePermissionsForModule("SubDetailItem"))
			.Concat(GeneratePermissionsForModule("SubDetailList"))
			
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

    public static class MainModule
	{
		public const string View = "Permission.MainModule.View";
		public const string Create = "Permission.MainModule.Create";
		public const string Edit = "Permission.MainModule.Edit";
		public const string Delete = "Permission.MainModule.Delete";
		public const string Approve = "Permission.MainModule.Approve";
	}
	public static class ParentModule
	{
		public const string View = "Permission.ParentModule.View";
		public const string Create = "Permission.ParentModule.Create";
		public const string Edit = "Permission.ParentModule.Edit";
		public const string Delete = "Permission.ParentModule.Delete";
	}
	public static class SubDetailItem
	{
		public const string View = "Permission.SubDetailItem.View";
		public const string Create = "Permission.SubDetailItem.Create";
		public const string Edit = "Permission.SubDetailItem.Edit";
		public const string Delete = "Permission.SubDetailItem.Delete";
		public const string Approve = "Permission.SubDetailItem.Approve";
	}
	public static class SubDetailList
	{
		public const string View = "Permission.SubDetailList.View";
		public const string Create = "Permission.SubDetailList.Create";
		public const string Edit = "Permission.SubDetailList.Edit";
		public const string Delete = "Permission.SubDetailList.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
