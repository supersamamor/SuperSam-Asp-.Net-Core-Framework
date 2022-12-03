namespace CTI.FAS.Web;

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
			.Concat(GeneratePermissionsForModule("Company"))
			.Concat(GeneratePermissionsForModule("Project"))
			.Concat(GeneratePermissionsForModule("Tenant"))
			.Concat(GeneratePermissionsForModule("UserEntity"))
			.Concat(GeneratePermissionsForModule("CheckReleaseOption"))
			.Concat(GeneratePermissionsForModule("Batch"))
			.Concat(GeneratePermissionsForModule("Generated"))
			.Concat(GeneratePermissionsForModule("Creditor"))
			.Concat(GeneratePermissionsForModule("CreditorEmail"))
			
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

    public static class DatabaseConnectionSetup
	{
		public const string View = "Permission.DatabaseConnectionSetup.View";
		public const string Create = "Permission.DatabaseConnectionSetup.Create";
		public const string Edit = "Permission.DatabaseConnectionSetup.Edit";
		public const string Delete = "Permission.DatabaseConnectionSetup.Delete";
		public const string Approve = "Permission.DatabaseConnectionSetup.Approve";
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
	public static class Tenant
	{
		public const string View = "Permission.Tenant.View";
		public const string Create = "Permission.Tenant.Create";
		public const string Edit = "Permission.Tenant.Edit";
		public const string Delete = "Permission.Tenant.Delete";
	}
	public static class UserEntity
	{
		public const string View = "Permission.UserEntity.View";
		public const string Create = "Permission.UserEntity.Create";
		public const string Edit = "Permission.UserEntity.Edit";
		public const string Delete = "Permission.UserEntity.Delete";
	}
	public static class CheckReleaseOption
	{
		public const string View = "Permission.CheckReleaseOption.View";
		public const string Create = "Permission.CheckReleaseOption.Create";
		public const string Edit = "Permission.CheckReleaseOption.Edit";
		public const string Delete = "Permission.CheckReleaseOption.Delete";
	}
	public static class Batch
	{
		public const string View = "Permission.Batch.View";
		public const string Create = "Permission.Batch.Create";
		public const string Edit = "Permission.Batch.Edit";
		public const string Delete = "Permission.Batch.Delete";
	}
	public static class Generated
	{
		public const string View = "Permission.Generated.View";
		public const string Create = "Permission.Generated.Create";
		public const string Edit = "Permission.Generated.Edit";
		public const string Delete = "Permission.Generated.Delete";
	}
	public static class Creditor
	{
		public const string View = "Permission.Creditor.View";
		public const string Create = "Permission.Creditor.Create";
		public const string Edit = "Permission.Creditor.Edit";
		public const string Delete = "Permission.Creditor.Delete";
	}
	public static class CreditorEmail
	{
		public const string View = "Permission.CreditorEmail.View";
		public const string Create = "Permission.CreditorEmail.Create";
		public const string Edit = "Permission.CreditorEmail.Edit";
		public const string Delete = "Permission.CreditorEmail.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
