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
			.Concat(GeneratePermissionsForModule("PaymentTransaction"))
			.Concat(GeneratePermissionsForModule("Creditor"))
			.Concat(GeneratePermissionsForModule("EnrolledPayee"))
			.Concat(GeneratePermissionsForModule("EnrolledPayeeEmail"))
			
			.Concat(GeneratePermissionsForModule("ApproverSetup"));
    }

    public static IEnumerable<string> GeneratePermissionsForModule(string module)
    {
        var permissions = new List<string>(); 
        if (module == "DatabaseConnectionSetup" || module == "Company" || module == "Project"
            || module == "Tenant" || module == "Creditor" || module == "PaymentTransaction")
        {
            permissions.Add($"Permission.{module}.View");
            permissions.Add($"Permission.{module}.Edit");
        }
        else if (module == "EnrolledPayee")
        {
            permissions.Add($"Permission.{module}.View");
            permissions.Add($"Permission.{module}.Create");
            permissions.Add($"Permission.{module}.Edit");
        }
        else if (module == "ApproverSetup")
        {
            permissions.Add($"Permission.{module}.Create");
            permissions.Add($"Permission.{module}.View");
            permissions.Add($"Permission.{module}.Edit");
            permissions.Add($"Permission.{module}.PendingApprovals");
        }
        else
        {
            permissions.Add($"Permission.{module}.View");
            permissions.Add($"Permission.{module}.Create");
            permissions.Add($"Permission.{module}.Edit");
            permissions.Add($"Permission.{module}.Delete");
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
		public const string Edit = "Permission.DatabaseConnectionSetup.Edit";
	}
	public static class Company
	{
		public const string View = "Permission.Company.View";		
		public const string Edit = "Permission.Company.Edit";		
	}
	public static class Project
	{
		public const string View = "Permission.Project.View";	
		public const string Edit = "Permission.Project.Edit";
	}
	public static class Tenant
	{
		public const string View = "Permission.Tenant.View";	
		public const string Edit = "Permission.Tenant.Edit";
	}
    public static class Creditor
    {
        public const string View = "Permission.Creditor.View";
        public const string Edit = "Permission.Creditor.Edit";
    }
    public static class PaymentTransaction
	{
		public const string View = "Permission.PaymentTransaction.View";		
		public const string Edit = "Permission.PaymentTransaction.Edit";
	}

	public static class EnrolledPayee
	{
		public const string View = "Permission.EnrolledPayee.View";
		public const string Create = "Permission.EnrolledPayee.Create";
		public const string Edit = "Permission.EnrolledPayee.Edit";	
	}	
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
