namespace CTI.ELMS.Application.Features.ELMS.TabNavigation.Models
{
    public class TabNavigationModel
    {
        public string TabName { get; set; } = "";
        public string LeadId { get; set; } = "";
        public string LeadName { get; set; } = "";
        public int ForAwardNoticeCount { get; set; }
        public int ForContractCount { get; set; }
    }
}
