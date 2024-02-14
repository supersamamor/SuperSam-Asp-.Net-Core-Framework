using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.ProjectPL.Web.Areas.Admin.Queries.AuditTrail;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.AuditTrail;
[Authorize]
public class IndexModel : BasePageModel<IndexModel>
{
    public IList<ChangeHistoryModel> ChangeHistoryList { get; set; } = new List<ChangeHistoryModel>();
    public async Task<IActionResult> OnGetChangesHistory(string auditlogsid)
    {
        JObject mergedJson = [];
        return await Mediatr.Send(new GetAuditLogByIdQuery(Convert.ToInt32(auditlogsid))).ToActionResult(
          some: e =>
          {
              AuditLogViewModel auditLog = new();
              Mapper.Map(e, auditLog);
              var mergedChanges = MergeChanges(auditLog.OldValues, auditLog.NewValues);
              ChangeHistoryList.Add(
                  new ChangeHistoryModel()
                  {
                      Sequence = 1,
                      Id = auditLog.Id,
                      Type = auditLog.Type,
                      TableName = auditLog.TableName,
                      PrimaryKey = auditLog.PrimaryKey,
                      MergedChanges = mergedChanges,
                  });
              return Partial("_ChangesHistory", ChangeHistoryList);
          },
          none: null);
    }
    private JObject MergeChanges(string oldData, string newData)
    {
        JObject oldJson = JObject.Parse(oldData);
        JObject newJson = JObject.Parse(newData);
        JObject mergedJson = new();
        foreach (var property in oldJson.Properties())
        {
            JToken oldValue = property.Value;
            JToken? newValue;
            if (newJson.TryGetValue(property.Name, out newValue))
            {
                if (!JToken.DeepEquals(oldValue, newValue))
                {
                    mergedJson.Add(property.Name, new JValue($"<span class=\"oldvalue\">{oldValue}</span><span class=\"newvalue\">{newValue}</span>"));
                }
                else
                {
                    mergedJson.Add(property.Name, oldValue);
                }
            }
            else
            {
                mergedJson.Add(property.Name, oldValue);
            }
        }
        return mergedJson;
    }
}
