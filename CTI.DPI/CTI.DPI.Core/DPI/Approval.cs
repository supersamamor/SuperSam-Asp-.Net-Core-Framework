using CTI.Common.Core.Base.Models;

namespace CTI.DPI.Core.DPI
{
    public record ApprovalState : BaseEntity
    {
        public string ApproverUserId { get; init; } = "";
        public string ApprovalRecordId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string Status { get; set; } = ApprovalStatus.New;
        public DateTime? StatusUpdateDateTime { get; set; }
        public string EmailSendingStatus { get; set; } = "";
        public string EmailSendingRemarks { get; set; } = "";
        public DateTime? EmailSendingDateTime { get; set; }
        public ApprovalRecordState ApprovalRecord { get; init; } = new ApprovalRecordState();
        public string? ApprovalRemarks { get; set; }
        public void SetToPendingEmail()
        {
            this.EmailSendingStatus = SendingStatus.Pending;
            this.EmailSendingDateTime = DateTime.UtcNow;
        }
        public void SendingDone()
        {
            this.EmailSendingStatus = SendingStatus.Done;
            this.EmailSendingDateTime = DateTime.UtcNow;
            this.EmailSendingRemarks = "";
            if (this.Status != ApprovalStatus.Approved)
            {
                this.Status = ApprovalStatus.ForApproval;
                this.StatusUpdateDateTime = DateTime.UtcNow;
            }
        }
        public void SendingFailed(string error)
        {
            this.EmailSendingDateTime = DateTime.UtcNow;
            this.EmailSendingStatus = SendingStatus.Failed;
            this.EmailSendingRemarks = error;
        }
        public void Approve(string remarks)
        {
            this.StatusUpdateDateTime = DateTime.UtcNow;
            this.Status = ApprovalStatus.Approved;
            this.ApprovalRemarks = remarks;
        }
        public void Reject(string remarks)
        {
            this.StatusUpdateDateTime = DateTime.UtcNow;
            this.Status = ApprovalStatus.Rejected;
            this.ApprovalRemarks = remarks;
        }
        public void Skip()
        {
            this.StatusUpdateDateTime = DateTime.UtcNow;
            this.Status = ApprovalStatus.Skipped;       
        }
    }
    public record ApprovalRecordState : BaseEntity
    {
        public string ApproverSetupId { get; init; } = ""; 
        public string DataId { get; init; } = "";
        public string Status { get; set; } = ApprovalStatus.New;
        public ApproverSetupState? ApproverSetup { get; init; }
        public IList<ApprovalState>? ApprovalList { get; set; }
        public void Approve()
        {
            this.Status = ApprovalStatus.Approved;
        }
        public void PartiallyApprove()
        {
            this.Status = ApprovalStatus.PartiallyApproved;
        }
        public void Reject()
        {
            this.Status = ApprovalStatus.Rejected;
        }
    }
    public record ApproverSetupState : BaseEntity
    {
        public string ApprovalSetupType { get; init; } = "";
        public string? WorkflowName { get; init; } = "";
        public string? WorkflowDescription { get; init; } = "";
        public string? TableName { get; init; } = "";
        public string ApprovalType { get; init; } = ApprovalTypes.InSequence;
        public string EmailSubject { get; init; } = "";
        public string EmailBody { get; init; } = "";
        public IList<ApproverAssignmentState>? ApproverAssignmentList { get; set; }
    }
    public record ApproverAssignmentState : BaseEntity
    {
        public string ApproverType { get; init; } = ApproverTypes.User;
        public string ApproverSetupId { get; init; } = "";
        public int Sequence { get; init; } = 0;
        public string? ApproverUserId { get; init; } = "";
        public string? ApproverRoleId { get; init; } = "";
        public ApproverSetupState ApproverSetup { get; init; } = new ApproverSetupState();
    }
    public static class ApprovalStatus
    {
        public const string New = "New";
        public const string ForApproval = "For Approval";
        public const string PartiallyApproved = "Partially Approved";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Skipped = "Skipped";
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
	public static class ApproverTypes
    {
        public const string User = "User";
        public const string Role = "Role";  
        public static readonly List<string> ApproverTypeList =
           new()
           {
               User,
               Role,            
           };
    }
    public static class ApprovalSetupTypes
    {
        public const string Modular = "Modular";
        public const string Workflow = "Workflow";
    }
	public static class ApprovalModule
	{
		public const string Report = "Report";
		public static readonly List<string> ApprovalTableList =
		new()
		{
			Report,
		};
	}
}
