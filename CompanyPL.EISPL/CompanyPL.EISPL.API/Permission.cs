namespace CompanyPL.EISPL.API;

public static class Permission
{
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