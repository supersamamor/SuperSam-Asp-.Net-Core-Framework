using CTI.Common.Core.Base.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder
{
    public record Approval : BaseEntity
    {
        public string ApproverSetupId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string ApproverUserId { get; init; } = "";
        public string Status { get; init; } = ApprovalStatus.New;
        public ApproverSetup ApproverSetup { get; init; } = new ApproverSetup();
    }
    public record ApproverSetup : BaseEntity
    {
        public string TableName { get; init; } = "";
        public string ApprovalType { get; init; } = ApprovalTypes.InSequence;
    }
    public record ApproverAssignment : BaseEntity
    {
        public string ApproverSetupId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string ApproverUserId { get; init; } = "";
        public ApproverSetup ApproverSetup { get; init; } = new ApproverSetup();
    }
    public static class ApprovalStatus
    {
        public const string New = "New";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
    }
    public static class ApprovalTypes
    {
        public const string Any = "Any";
        public const string All = "All";
        public const string InSequence = "InSequence";
    }
}
