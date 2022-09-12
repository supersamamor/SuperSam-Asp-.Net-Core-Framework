namespace CTI.ContractManagement.Web;

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
            .Concat(GeneratePermissionsForModule("ApplicationConfiguration"))
			.Concat(GeneratePermissionsForModule("MilestoneStage"))
			.Concat(GeneratePermissionsForModule("Frequency"))
			.Concat(GeneratePermissionsForModule("PricingType"))
			.Concat(GeneratePermissionsForModule("ProjectCategory"))
			.Concat(GeneratePermissionsForModule("Deliverable"))
			.Concat(GeneratePermissionsForModule("Client"))
			.Concat(GeneratePermissionsForModule("Project"))
			.Concat(GeneratePermissionsForModule("ProjectDeliverable"))
			.Concat(GeneratePermissionsForModule("ProjectMilestone"))
			.Concat(GeneratePermissionsForModule("ProjectPackage"))
			.Concat(GeneratePermissionsForModule("ProjectPackageAdditionalDeliverable"))
			.Concat(GeneratePermissionsForModule("ProjectHistory"))
			.Concat(GeneratePermissionsForModule("ProjectDeliverableHistory"))
			.Concat(GeneratePermissionsForModule("ProjectMilestoneHistory"))
			.Concat(GeneratePermissionsForModule("ProjectPackageHistory"))
			.Concat(GeneratePermissionsForModule("ProjectPackageAdditionalDeliverableHistory"))
			
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

    public static class ApplicationConfiguration
	{
		public const string View = "Permission.ApplicationConfiguration.View";
		public const string Create = "Permission.ApplicationConfiguration.Create";
		public const string Edit = "Permission.ApplicationConfiguration.Edit";
		public const string Delete = "Permission.ApplicationConfiguration.Delete";
	}
	public static class MilestoneStage
	{
		public const string View = "Permission.MilestoneStage.View";
		public const string Create = "Permission.MilestoneStage.Create";
		public const string Edit = "Permission.MilestoneStage.Edit";
		public const string Delete = "Permission.MilestoneStage.Delete";
	}
	public static class Frequency
	{
		public const string View = "Permission.Frequency.View";
		public const string Create = "Permission.Frequency.Create";
		public const string Edit = "Permission.Frequency.Edit";
		public const string Delete = "Permission.Frequency.Delete";
	}
	public static class PricingType
	{
		public const string View = "Permission.PricingType.View";
		public const string Create = "Permission.PricingType.Create";
		public const string Edit = "Permission.PricingType.Edit";
		public const string Delete = "Permission.PricingType.Delete";
	}
	public static class ProjectCategory
	{
		public const string View = "Permission.ProjectCategory.View";
		public const string Create = "Permission.ProjectCategory.Create";
		public const string Edit = "Permission.ProjectCategory.Edit";
		public const string Delete = "Permission.ProjectCategory.Delete";
	}
	public static class Deliverable
	{
		public const string View = "Permission.Deliverable.View";
		public const string Create = "Permission.Deliverable.Create";
		public const string Edit = "Permission.Deliverable.Edit";
		public const string Delete = "Permission.Deliverable.Delete";
	}
	public static class Client
	{
		public const string View = "Permission.Client.View";
		public const string Create = "Permission.Client.Create";
		public const string Edit = "Permission.Client.Edit";
		public const string Delete = "Permission.Client.Delete";
	}
	public static class Project
	{
		public const string View = "Permission.Project.View";
		public const string Create = "Permission.Project.Create";
		public const string Edit = "Permission.Project.Edit";
		public const string Delete = "Permission.Project.Delete";
		public const string Approve = "Permission.Project.Approve";
	}
	public static class ProjectDeliverable
	{
		public const string View = "Permission.ProjectDeliverable.View";
		public const string Create = "Permission.ProjectDeliverable.Create";
		public const string Edit = "Permission.ProjectDeliverable.Edit";
		public const string Delete = "Permission.ProjectDeliverable.Delete";
	}
	public static class ProjectMilestone
	{
		public const string View = "Permission.ProjectMilestone.View";
		public const string Create = "Permission.ProjectMilestone.Create";
		public const string Edit = "Permission.ProjectMilestone.Edit";
		public const string Delete = "Permission.ProjectMilestone.Delete";
	}
	public static class ProjectPackage
	{
		public const string View = "Permission.ProjectPackage.View";
		public const string Create = "Permission.ProjectPackage.Create";
		public const string Edit = "Permission.ProjectPackage.Edit";
		public const string Delete = "Permission.ProjectPackage.Delete";
	}
	public static class ProjectPackageAdditionalDeliverable
	{
		public const string View = "Permission.ProjectPackageAdditionalDeliverable.View";
		public const string Create = "Permission.ProjectPackageAdditionalDeliverable.Create";
		public const string Edit = "Permission.ProjectPackageAdditionalDeliverable.Edit";
		public const string Delete = "Permission.ProjectPackageAdditionalDeliverable.Delete";
	}
	public static class ProjectHistory
	{
		public const string View = "Permission.ProjectHistory.View";
		public const string Create = "Permission.ProjectHistory.Create";
		public const string Edit = "Permission.ProjectHistory.Edit";
		public const string Delete = "Permission.ProjectHistory.Delete";
	}
	public static class ProjectDeliverableHistory
	{
		public const string View = "Permission.ProjectDeliverableHistory.View";
		public const string Create = "Permission.ProjectDeliverableHistory.Create";
		public const string Edit = "Permission.ProjectDeliverableHistory.Edit";
		public const string Delete = "Permission.ProjectDeliverableHistory.Delete";
	}
	public static class ProjectMilestoneHistory
	{
		public const string View = "Permission.ProjectMilestoneHistory.View";
		public const string Create = "Permission.ProjectMilestoneHistory.Create";
		public const string Edit = "Permission.ProjectMilestoneHistory.Edit";
		public const string Delete = "Permission.ProjectMilestoneHistory.Delete";
	}
	public static class ProjectPackageHistory
	{
		public const string View = "Permission.ProjectPackageHistory.View";
		public const string Create = "Permission.ProjectPackageHistory.Create";
		public const string Edit = "Permission.ProjectPackageHistory.Edit";
		public const string Delete = "Permission.ProjectPackageHistory.Delete";
	}
	public static class ProjectPackageAdditionalDeliverableHistory
	{
		public const string View = "Permission.ProjectPackageAdditionalDeliverableHistory.View";
		public const string Create = "Permission.ProjectPackageAdditionalDeliverableHistory.Create";
		public const string Edit = "Permission.ProjectPackageAdditionalDeliverableHistory.Edit";
		public const string Delete = "Permission.ProjectPackageAdditionalDeliverableHistory.Delete";
	}
	
	public static class ApproverSetup
	{
		public const string Create = "Permission.ApproverSetup.Create";
		public const string View = "Permission.ApproverSetup.View";
		public const string Edit = "Permission.ApproverSetup.Edit";
		public const string PendingApprovals = "Permission.ApproverSetup.PendingApprovals";
	}
}
