using CompanyPL.ProjectPL.Core.ProjectPL;
using Newtonsoft.Json.Linq;
namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models
{
    public class ChangeHistoryMainModel
    {
        public string? DateTimeDuration { get; set; } = "";
        public string? DateTimeFormatted { get; set; } = "";
        public string? UserId { get; set; } = "";
        public IList<ChangeHistoryModel> ChangeHistoryList { get; set; } = new List<ChangeHistoryModel>();
    }
    public class ChangeHistoryModel
    {
        public int Sequence { get; set; }
        public int Id { get; set; }
        public string? Type { get; set; } = "";
        public string? TableName { get; set; } = "";
        public string? PrimaryKey { get; set; } = "";
        public JObject MergedChanges { get; set; } = [];
        public bool HasChanges { get; set; } = false;
        public string? AuditTypeBadge
        {
            get
            {
                if (!HasChanges)
                {
                    return @"<span class=""badge bg-secondary"">No Changes</span>";
                }
                if (Enum.TryParse(this.Type, out Common.Data.AuditType auditType))
                {
                    switch (auditType)
                    {
                        case Common.Data.AuditType.Update:
                            return @"<span class=""badge bg-primary"">Updated</span>";
                        case Common.Data.AuditType.Create:
                            return @"<span class=""badge bg-success"">Created</span>";
                        case Common.Data.AuditType.Delete:
                            return @"<span class=""badge bg-danger"">Deleted</span>";
                        default:
                            break;
                    }
                }
                return "";
            }
        }
    }
}
