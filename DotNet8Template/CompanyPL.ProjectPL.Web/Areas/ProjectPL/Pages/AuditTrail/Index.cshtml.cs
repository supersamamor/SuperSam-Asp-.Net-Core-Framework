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
              try
              {
                  AuditLogViewModel auditLog = new();
                  Mapper.Map(e, auditLog);
                  var mergedChanges = MergeChanges(auditLog.Type, auditLog.OldValues, auditLog.NewValues);
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
              }
              catch (Exception ex)
              {
                  var test = 1;
              }
              return Partial("_ChangesHistory", ChangeHistoryList);
          },
          none: null);
    }
    private JObject MergeChanges(string? type, string oldData, string newData)
    {
        JObject? oldJson = null;
        JObject? newJson = null;
        if (!string.IsNullOrEmpty(oldData))
        {
            oldJson = JObject.Parse(oldData);
        }
        if (!string.IsNullOrEmpty(newData))
        {
            newJson = JObject.Parse(newData);
        }
        JObject mergedJson = [];
        if (Enum.TryParse(type, out Common.Data.AuditType auditType))
        {
            if (auditType == Common.Data.AuditType.Delete)
            {
                foreach (var property in oldJson!.Properties())
                {
                    mergedJson.Add(property.Name, property.Value);
                }
            }
            else
            {
                foreach (var property in newJson!.Properties())
                {
                    JToken newValue = property.Value;
                    JToken? oldValue;

                    if (oldJson != null && oldJson.TryGetValue(property.Name, out oldValue))
                    {
                        if (!JToken.DeepEquals(oldValue, newValue))
                        {
                            mergedJson.Add(property.Name, new JValue($"<span class=\"oldvalue\">{oldValue}</span><span class=\"newvalue\">{newValue}</span>"));
                        }
                        else
                        {
                            mergedJson.Add(property.Name, newValue);
                        }
                    }
                    else
                    {
                        mergedJson.Add(property.Name, newValue);
                    }
                }
            }
        }

        return mergedJson;
    }
}
