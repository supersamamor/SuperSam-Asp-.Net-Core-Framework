namespace CompanyPL.EISPL.Web;

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
            .Concat(GeneratePermissionsForModule("PLEmployee"))
			.Concat(GeneratePermissionsForModule("PLContactInformation"))
			.Concat(GeneratePermissionsForModule("PLHealthDeclaration"))
			.Concat(GeneratePermissionsForModule("Test"))
			;
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

    public static class PLEmployee
	{
		public const string View = "Permission.PLEmployee.View";
		public const string Create = "Permission.PLEmployee.Create";
		public const string Edit = "Permission.PLEmployee.Edit";
		public const string Delete = "Permission.PLEmployee.Delete";
	}
	public static class PLContactInformation
	{
		public const string View = "Permission.PLContactInformation.View";
		public const string Create = "Permission.PLContactInformation.Create";
		public const string Edit = "Permission.PLContactInformation.Edit";
		public const string Delete = "Permission.PLContactInformation.Delete";
	}
	public static class PLHealthDeclaration
	{
		public const string View = "Permission.PLHealthDeclaration.View";
		public const string Create = "Permission.PLHealthDeclaration.Create";
		public const string Edit = "Permission.PLHealthDeclaration.Edit";
		public const string Delete = "Permission.PLHealthDeclaration.Delete";
	}
	public static class Test
	{
		public const string View = "Permission.Test.View";
		public const string Create = "Permission.Test.Create";
		public const string Edit = "Permission.Test.Edit";
		public const string Delete = "Permission.Test.Delete";
	}
	
	
}
