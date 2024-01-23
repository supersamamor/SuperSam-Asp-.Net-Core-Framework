namespace CompanyPL.ProjectPL.API;

public static class Permission
{
    public static class Employee
	{
		public const string View = "Permission.Employee.View";
		public const string Create = "Permission.Employee.Create";
		public const string Edit = "Permission.Employee.Edit";
		public const string Delete = "Permission.Employee.Delete";
		public const string Upload = "Permission.Employee.Upload";
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
	
}