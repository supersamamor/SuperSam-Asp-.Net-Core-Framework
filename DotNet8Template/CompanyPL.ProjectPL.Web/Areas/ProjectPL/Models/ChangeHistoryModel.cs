using CompanyPL.ProjectPL.Core.ProjectPL;
using Newtonsoft.Json.Linq;
namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models
{
    public class ChangeHistoryModel
    {
        public int Sequence { get; set; }
        public int Id { get; set; }
        public string? Type { get; set; } = "";
        public string? TableName { get; set; } = "";
        public string? PrimaryKey { get; set; } = "";
        public JObject MergedChanges { get; set; } = [];
        public string? AuditTypeBadge
        {
            get
            {

                if (Enum.TryParse(this.Type, out Common.Data.AuditType auditType))
                {
                    switch (auditType)
                    {
                        case Common.Data.AuditType.Update:
                            return @"<span class=""badge bg-primary"">" + auditType + "</span>";
                        case Common.Data.AuditType.Create:
                            return @"<span class=""badge bg-success"">" + auditType + "</span>";
                        case Common.Data.AuditType.Delete:
                            return @"<span class=""badge bg-danger"">" + auditType + "</span>";
                        default:
                            break;
                    }
                }
                return "";
            }
        }
    }
}
