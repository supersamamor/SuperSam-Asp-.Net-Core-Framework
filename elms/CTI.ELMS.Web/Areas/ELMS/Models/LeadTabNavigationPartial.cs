namespace CTI.ELMS.Web.Areas.ELMS.Models
{
    public class LeadTabNavigationPartial
    {
        public string TabName { get; set; } = "";
        public string LeadId { get; set; } = "";
        public string LeadName { get; set; } = "";   
        public int ForAwardNoticeCount { get; set; }
        public int ForContractCount { get; set; }
    }
}
