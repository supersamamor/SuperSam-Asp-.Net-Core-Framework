namespace CTI.FAS.API;

public static class Permission
{
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
	
}