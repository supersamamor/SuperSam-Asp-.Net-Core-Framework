namespace CTI.SQLReportAutoSender.Web;

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
            .Concat(GeneratePermissionsForModule("ScheduleFrequency"))
			.Concat(GeneratePermissionsForModule("ScheduleParameter"))
			.Concat(GeneratePermissionsForModule("ScheduleFrequencyParameterSetup"))
			.Concat(GeneratePermissionsForModule("Report"))
			.Concat(GeneratePermissionsForModule("ReportDetail"))
			.Concat(GeneratePermissionsForModule("MailSetting"))
			.Concat(GeneratePermissionsForModule("MailRecipient"))
			.Concat(GeneratePermissionsForModule("ReportScheduleSetting"))
			.Concat(GeneratePermissionsForModule("CustomSchedule"))
			.Concat(GeneratePermissionsForModule("ReportInbox"))
			
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

    public static class ScheduleFrequency
	{
		public const string View = "Permission.ScheduleFrequency.View";
		public const string Create = "Permission.ScheduleFrequency.Create";
		public const string Edit = "Permission.ScheduleFrequency.Edit";
		public const string Delete = "Permission.ScheduleFrequency.Delete";
	}
	public static class ScheduleParameter
	{
		public const string View = "Permission.ScheduleParameter.View";
		public const string Create = "Permission.ScheduleParameter.Create";
		public const string Edit = "Permission.ScheduleParameter.Edit";
		public const string Delete = "Permission.ScheduleParameter.Delete";
	}
	public static class ScheduleFrequencyParameterSetup
	{
		public const string View = "Permission.ScheduleFrequencyParameterSetup.View";
		public const string Create = "Permission.ScheduleFrequencyParameterSetup.Create";
		public const string Edit = "Permission.ScheduleFrequencyParameterSetup.Edit";
		public const string Delete = "Permission.ScheduleFrequencyParameterSetup.Delete";
	}
	public static class Report
	{
		public const string View = "Permission.Report.View";
		public const string Create = "Permission.Report.Create";
		public const string Edit = "Permission.Report.Edit";
		public const string Delete = "Permission.Report.Delete";
		public const string Approve = "Permission.Report.Approve";
	}
	public static class ReportDetail
	{
		public const string View = "Permission.ReportDetail.View";
		public const string Create = "Permission.ReportDetail.Create";
		public const string Edit = "Permission.ReportDetail.Edit";
		public const string Delete = "Permission.ReportDetail.Delete";
	}
	public static class MailSetting
	{
		public const string View = "Permission.MailSetting.View";
		public const string Create = "Permission.MailSetting.Create";
		public const string Edit = "Permission.MailSetting.Edit";
		public const string Delete = "Permission.MailSetting.Delete";
	}
	public static class MailRecipient
	{
		public const string View = "Permission.MailRecipient.View";
		public const string Create = "Permission.MailRecipient.Create";
		public const string Edit = "Permission.MailRecipient.Edit";
		public const string Delete = "Permission.MailRecipient.Delete";
	}
	public static class ReportScheduleSetting
	{
		public const string View = "Permission.ReportScheduleSetting.View";
		public const string Create = "Permission.ReportScheduleSetting.Create";
		public const string Edit = "Permission.ReportScheduleSetting.Edit";
		public const string Delete = "Permission.ReportScheduleSetting.Delete";
	}
	public static class CustomSchedule
	{
		public const string View = "Permission.CustomSchedule.View";
		public const string Create = "Permission.CustomSchedule.Create";
		public const string Edit = "Permission.CustomSchedule.Edit";
		public const string Delete = "Permission.CustomSchedule.Delete";
	}
	public static class ReportInbox
	{
		public const string View = "Permission.ReportInbox.View";
		public const string Create = "Permission.ReportInbox.Create";
		public const string Edit = "Permission.ReportInbox.Edit";
		public const string Delete = "Permission.ReportInbox.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
