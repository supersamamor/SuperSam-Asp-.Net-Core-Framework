using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Areas.Admin.Models;
using CompanyPL.ProjectPL.Web.Areas.Admin.Queries.AuditTrail;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.AuditTrail;
[Authorize]
public class IndexModel : BasePageModel<IndexModel>
{
    public ChangeHistoryMainModel ChangeHistory { get; set; } = new ChangeHistoryMainModel();
    public async Task<IActionResult> OnGetChangesHistory(string auditlogsid, string moduleName)
    {
        JObject mergedJson = [];
        return await Mediatr.Send(new GetAuditLogByIdQuery(Convert.ToInt32(auditlogsid))).ToActionResult(
            someAsync: async e =>
            {
                AuditLogViewModel auditLog = new();
                Mapper.Map(e, auditLog);
                ChangeHistory.DateTimeDuration = auditLog.DateTimeDuration;
                ChangeHistory.DateTimeFormatted = auditLog.DateTimeFormatted;
                ChangeHistory.UserId = auditLog.UserId;
                var mergedChanges = MergeChanges(auditLog.Type, auditLog.OldValues, auditLog.NewValues, moduleName);
                ChangeHistory.ChangeHistoryList.Add(
                    new ChangeHistoryModel()
                    {
                        Sequence = 1,
                        Id = auditLog.Id,
                        Type = auditLog.Type,
                        TableName = auditLog.TableName,
                        PrimaryKey = auditLog.PrimaryKey,
                        MergedChanges = mergedChanges,
                    });
                var auditLogsList = await Mediatr.Send(new GetAuditLogsByTraceIdQuery(auditLog.TraceId!, auditLog.PrimaryKey!));
                int counter = 2;
                foreach (var auditLogsItem in auditLogsList)
                {
                    mergedChanges = MergeChanges(auditLogsItem.Type, auditLogsItem.OldValues, auditLogsItem.NewValues, moduleName);
                    ChangeHistory.ChangeHistoryList.Add(
                       new ChangeHistoryModel()
                       {
                           Sequence = counter,
                           Id = auditLogsItem.Id,
                           Type = auditLogsItem.Type,
                           TableName = auditLogsItem.TableName,
                           PrimaryKey = auditLogsItem.PrimaryKey,
                           MergedChanges = mergedChanges,
                       });
                    counter++;
                }
                return Partial("_ChangesHistory", ChangeHistory);
            },
          none: null);
    }
    private JObject MergeChanges(string? type, string? oldData, string? newData, string moduleName)
    {
        moduleName += "Id";
        Type baseEntityType = typeof(Common.Core.Base.Models.BaseEntity);
        // Get all the fields of the BaseEntity class          
        var excludedProperties = baseEntityType.GetProperties().Select(l => l.Name);
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
                    if (!excludedProperties.Contains(property.Name, StringComparer.OrdinalIgnoreCase)
                        && !string.Equals(moduleName, property.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        mergedJson.Add(property.Name, property.Value);
                    }
                }
            }
            else
            {
                foreach (var property in newJson!.Properties())
                {
                    if (!excludedProperties.Contains(property.Name, StringComparer.OrdinalIgnoreCase)
                        && !string.Equals(moduleName, property.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        JToken newValue = property.Value;
                        if (oldJson != null && oldJson.TryGetValue(property.Name, out JToken? oldValue))
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
        }
        return mergedJson;
    }
}
