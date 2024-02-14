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
    }
}
