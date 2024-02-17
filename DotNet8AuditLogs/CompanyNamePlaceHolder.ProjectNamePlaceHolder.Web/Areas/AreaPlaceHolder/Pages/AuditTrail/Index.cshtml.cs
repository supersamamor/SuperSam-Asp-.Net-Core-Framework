using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.AuditTrail;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.AuditTrail;
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
                ChangeHistory.ChangeHistoryList.Add(MergeChanges(auditLog, auditLog.OldValues, moduleName, 1));
                var auditLogsList = await Mediatr.Send(new GetAuditLogsByTraceIdQuery(auditLog.TraceId!, auditLog.PrimaryKey!));
                //Temporay Solution because old values was not logged properly on sub collections
                var previousAuditLogsList = await Mediatr.Send(new GetPreviousAuditLogsByTraceIdQuery(auditLog.TraceId!, auditLog.PrimaryKey!, auditLog.DateTime));
                int counter = 2;
                foreach (var auditLogsItem in auditLogsList)
                {
                    var previousAuditLogs = previousAuditLogsList.Where(l => l.PrimaryKey == auditLogsItem.PrimaryKey).FirstOrDefault();
                    var oldValues = string.IsNullOrEmpty(previousAuditLogs?.NewValues) ? previousAuditLogs?.OldValues : previousAuditLogs.NewValues;
                    ChangeHistory.ChangeHistoryList.Add(MergeChanges(auditLogsItem, oldValues, moduleName, counter));                   
                    counter++;
                }
                return Partial("_ChangesHistory", ChangeHistory);
            },
          none: null);
    }
    private ChangeHistoryModel MergeChanges(AuditLogViewModel? auditLog, string? oldData, string moduleName, int sequence)
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
        if (!string.IsNullOrEmpty(auditLog?.NewValues))
        {
            newJson = JObject.Parse(auditLog?.NewValues!);
        }
        JObject mergedJson = [];
        bool hasChanges = false;
        if (Enum.TryParse(auditLog?.Type, out Common.Data.AuditType auditType))
        {
            if (auditType == Common.Data.AuditType.Delete)
            {
                foreach (var property in oldJson!.Properties())
                {
                    if (!excludedProperties.Contains(property.Name, StringComparer.OrdinalIgnoreCase)
                        && !string.Equals(moduleName, property.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        hasChanges = true;
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
                                hasChanges = true;
                                mergedJson.Add(property.Name, new JValue($"<span class=\"oldvalue\">{oldValue}</span><span class=\"newvalue\">{newValue}</span>"));
                            }
                            else
                            {                                
                                mergedJson.Add(property.Name, newValue);
                            }
                        }
                        else
                        {
                            hasChanges = true;
                            mergedJson.Add(property.Name, newValue);
                        }
                    }
                }
            }
        }
        return new ChangeHistoryModel()
        {
            Sequence = sequence,
            Id = auditLog!.Id,
            Type = auditLog.Type,
            TableName = SplitPascalCaseAndRemoveState(auditLog.TableName!),
            PrimaryKey = auditLog.PrimaryKey,
            MergedChanges = mergedJson,
            HasChanges = hasChanges,
        };      
    }
    private static string SplitPascalCaseAndRemoveState(string input)
    {
        // Split PascalCase into separate words
        var words = Regex.Matches(input, @"([A-Z][a-z]*)")
                         .OfType<Match>()
                         .Select(m => m.Value)
                         .ToList();

        // Remove 'State' if it's the last word
        if (words.Count > 0 && words[words.Count - 1].ToLower() == "state")
        {
            words.RemoveAt(words.Count - 1);
        }

        // Join the words with space
        var result = string.Join(" ", words);

        return result;
    }
}
