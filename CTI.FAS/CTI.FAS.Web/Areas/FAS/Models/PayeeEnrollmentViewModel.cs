using CTI.FAS.Web.Models;

namespace CTI.FAS.Web.Areas.FAS.Models
{
    public record PayeeEnrollmentViewModel : BaseViewModel
	{
		public string? Company { get; set; }	
		public string Creditor{ get; init; } = "";	
		public string PayeeAccountType { get; init; } = "";	
		public string? Status { get; init; }
		public bool Enabled { get; set; }
	}
}
