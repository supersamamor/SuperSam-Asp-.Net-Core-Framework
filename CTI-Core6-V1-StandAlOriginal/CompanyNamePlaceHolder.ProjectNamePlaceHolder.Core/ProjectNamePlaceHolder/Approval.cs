using CTI.Common.Core.Base.Models;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder
{
    public record ApprovalState : BaseEntity
    {
        public string ApproverUserId { get; init; } = "";
        public string ApprovalRecordId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string Status { get; init; } = ApprovalStatus.New;
        public string EmailSendingStatus { get; set; } = "";
        public string EmailSendingRemarks { get; set; } = "";
        public ApprovalRecordState ApprovalRecord { get; init; } = new ApprovalRecordState();
        public void SetToPendingEmail()
        {
            this.EmailSendingStatus = SendingStatus.Pending;
        }
        public void SendingDone()
        {
            this.EmailSendingStatus = SendingStatus.Done;
        }
        public void SendingFailed(string error)
        {
            this.EmailSendingStatus = SendingStatus.Failed;
            this.EmailSendingRemarks = error;
        }
    }
    public record ApprovalRecordState : BaseEntity
    {
        public string ApproverSetupId { get; init; } = "";
        public string DataId { get; init; } = "";
        public string Status { get; init; } = ApprovalStatus.New;
        public ApproverSetupState ApproverSetup { get; init; } = new ApproverSetupState();
        public IList<ApprovalState>? ApprovalList { get; set; }
    }
    public record ApproverSetupState : BaseEntity
    {
        public string TableName { get; init; } = "";
        public string ApprovalType { get; init; } = ApprovalTypes.InSequence;
        public IList<ApproverAssignmentState>? ApproverAssignmentList { get; set; }
    }
    public record ApproverAssignmentState : BaseEntity
    {
        public string ApproverSetupId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string ApproverUserId { get; init; } = "";
        public ApproverSetupState ApproverSetup { get; init; } = new ApproverSetupState();
    }
    public static class ApprovalStatus
    {
        public const string New = "New";
        public const string PartiallyApproved = "Partially Approved";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public static readonly List<string> ApprovalStatusList =
              new()
              {
                  New,
                  Approved,
                  Rejected,
              };
    }
    public static class ApprovalTypes
    {
        public const string Any = "Any";
        public const string All = "All";
        public const string InSequence = "In Sequence";
        public static readonly List<string> ApprovalTypeList =
           new()
           {
               Any,
               All,
               InSequence,
           };
    }
    public static class SendingStatus
    {
        public const string Pending = "Pending";
        public const string Done = "Done";
        public const string Failed = "Failed";
    }
    public static class ApprovalModule
    {
        public const string MainModulePlaceHolder = "MainModulePlaceHolder";
        public static readonly List<string> MainModulePlaceHolderList =
        new()
        {
            MainModulePlaceHolder,
        };
    }
}
