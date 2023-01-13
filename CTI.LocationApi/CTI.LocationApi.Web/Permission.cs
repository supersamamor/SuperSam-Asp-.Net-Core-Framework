namespace CTI.LocationApi.Web;

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
            .Concat(GeneratePermissionsForModule("Barangay"))
			.Concat(GeneratePermissionsForModule("City"))
			.Concat(GeneratePermissionsForModule("Location"))
			.Concat(GeneratePermissionsForModule("Province"))
			.Concat(GeneratePermissionsForModule("Region"))
			.Concat(GeneratePermissionsForModule("Country"))
			
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

    public static class Barangay
	{
		public const string View = "Permission.Barangay.View";
		public const string Create = "Permission.Barangay.Create";
		public const string Edit = "Permission.Barangay.Edit";
		public const string Delete = "Permission.Barangay.Delete";
	}
	public static class City
	{
		public const string View = "Permission.City.View";
		public const string Create = "Permission.City.Create";
		public const string Edit = "Permission.City.Edit";
		public const string Delete = "Permission.City.Delete";
	}
	public static class Location
	{
		public const string View = "Permission.Location.View";
		public const string Create = "Permission.Location.Create";
		public const string Edit = "Permission.Location.Edit";
		public const string Delete = "Permission.Location.Delete";
	}
	public static class Province
	{
		public const string View = "Permission.Province.View";
		public const string Create = "Permission.Province.Create";
		public const string Edit = "Permission.Province.Edit";
		public const string Delete = "Permission.Province.Delete";
	}
	public static class Region
	{
		public const string View = "Permission.Region.View";
		public const string Create = "Permission.Region.Create";
		public const string Edit = "Permission.Region.Edit";
		public const string Delete = "Permission.Region.Delete";
	}
	public static class Country
	{
		public const string View = "Permission.Country.View";
		public const string Create = "Permission.Country.Create";
		public const string Edit = "Permission.Country.Edit";
		public const string Delete = "Permission.Country.Delete";
		public const string Approve = "Permission.Country.Approve";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
