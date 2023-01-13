namespace CTI.LocationApi.API;

public static class Permission
{
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
	
}