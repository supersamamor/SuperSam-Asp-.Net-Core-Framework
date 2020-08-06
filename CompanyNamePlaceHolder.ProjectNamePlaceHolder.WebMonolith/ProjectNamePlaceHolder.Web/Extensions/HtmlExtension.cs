using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectNamePlaceHolder.Application;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class HtmlExtension
    {
        private static string _sortBy { get; set; }
        private static string _orderBy { get; set; }
        private static string _fieldName { get; set; }
        private static string _pageName { get; set; }
        public static IHtmlContent PageSorter<TProperty>(this IHtmlHelper htmlHelper, Expression<Func<object, TProperty>> expression, int? maxwidth = null)
        {
            var propertyGetExpression = expression.Body as MemberExpression;
            var fieldOnClosureExpression = propertyGetExpression.Expression;
            MemberInfo property = fieldOnClosureExpression.Type.GetProperty(propertyGetExpression.Member.Name);
            var field = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            string fieldDisplayName = "[Field Not Found]";
            if (field != null)
            {
                _fieldName = property.Name;
                var _labelName = field.Name;          
                ResourceManager rm = new ResourceManager(field.ResourceType.ToString(), typeof(Resource).Assembly);
                fieldDisplayName = rm.GetString(_labelName);
            }
            string routes = CreateRoutes(htmlHelper.ViewData.Model);
            var sortIcon = "fas fa-sort";
            if (_sortBy == _fieldName)
            {
                sortIcon = "fas fa-sort-down";
                if (_orderBy == "Asc")
                {
                    sortIcon = "fas fa-sort-up";
                }
            }
            string maxwidthsytle = maxwidth != null ? @"style=""max-width:" + maxwidth + @"px;width:" + maxwidth + @"px;""" : "";
            var htmlstring = @"<th " + maxwidthsytle + @">";
            htmlstring += @"        <i class=""" + sortIcon + @"""></i>";
            htmlstring += @"        <a href=""" + _pageName + @"?";
            htmlstring += @"             " + routes + @"""";
            htmlstring += @"             class=""page-sorter""> " + fieldDisplayName;
            htmlstring += @"        </a>";
            htmlstring += @"   </th>";
            return new HtmlString(htmlstring);
        }
        public static IHtmlContent PromptConfirmationModal(this IHtmlHelper htmlHelper, string modalId, string triggerShowElementId, string triggerActionElement,
              string formId, string promptMessageContainerId, string message)
        {
            var htmlstring = @"";
            htmlstring += @"      <style> ";
            htmlstring += @"           @keyframes spin {";
            htmlstring += @"                0% {transform: rotate(0deg);}";
            htmlstring += @"                100% { transform: rotate(360deg);}";
            htmlstring += @"           }";
            htmlstring += @"      </style>";       
            htmlstring += @"      ";
            htmlstring += @"      <div id=""" + modalId + @"_Loader"" style=""display:none;width: 100%;height: 100%;position: fixed;z-index: 9999;background:#000;opacity:0.3;top:0px;left:0px;"">";
            htmlstring += @"           <div style=""border: 10px solid #f3f3f3;border-top: 10px solid #3498db;border-radius: 50%;width: 60px;height: 60px;animation: spin 2s linear infinite;margin: 0;top: 41%;left: 45%; -ms-transform: translate(-50%, -50%);transform: translate(-50%, -50%);position: absolute;""></div>";
            htmlstring += @"      </div>";
            htmlstring += @"      ";
            htmlstring += @"      <div class=""modal fade"" id=""" + modalId + @""" style=""position:fixed;top:20%;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + message + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top""";
            htmlstring += @"                               title=""Ok"" onclick=""$('#" + modalId + @"_Loader').show();$('#" + modalId + @"   ').modal('hide');$('#" + triggerActionElement + @"   ').click();"">";
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-dismiss=""modal"" data-toggle=""tooltip"" data-placement=""top"" title=""Close"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                <div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"";
            htmlstring += @"      <script type=""text/javascript"">";
            if (formId == "")
            {
                htmlstring += @"       function ShowModal" + modalId + @"() {";
                htmlstring += @"            $(""#" + modalId + @""").modal('show');";
                htmlstring += @"       }";
            }
            else
            {
                htmlstring += @"       function ShowModal" + modalId + @"() {";
                htmlstring += @"            var form = $('#" + formId + @"');";
                htmlstring += @"            if ($(form).valid()) {";
                htmlstring += @"                 $(""#" + modalId + @""").modal('show');";
                htmlstring += @"            }";
                htmlstring += @"            else";
                htmlstring += @"            {";
                htmlstring += @"                 $('#" + promptMessageContainerId + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>');";
                htmlstring += @"            }";
                htmlstring += @"       }";
            }
            htmlstring += @"           $( ""#" + triggerShowElementId + @""" ).bind( ""click"", function() {";
            htmlstring += @"                ShowModal" + modalId + @"();";
            htmlstring += @"           });";
            htmlstring += @"           ";
            htmlstring += @"           $(document).ready(function() {";
            htmlstring += @"                $(window).keydown(function(event){";
            htmlstring += @"                     if(event.keyCode == 13 && !event.shiftKey) {";
            htmlstring += @"                          event.preventDefault();";
            htmlstring += @"                          ShowModal" + modalId + @"();";
            htmlstring += @"                          return false;";
            htmlstring += @"                     }";
            htmlstring += @"                });";
            htmlstring += @"           });";
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }
        public static IHtmlContent DisplayLabelWithRequiredTag<TProperty>(this IHtmlHelper htmlHelper, Expression<Func<object, TProperty>> expression, string className = null)
        {
            var propertyGetExpression = expression.Body as MemberExpression;
            var fieldOnClosureExpression = propertyGetExpression.Expression;
            MemberInfo property = fieldOnClosureExpression.Type.GetProperty(propertyGetExpression.Member.Name);
            var field = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            var requiredAttribute = property.GetCustomAttribute(typeof(RequiredAttribute)) as RequiredAttribute;
            string fieldDisplayName = "[Field Not Found]";
            if (field != null)
            {           
                var _labelName = field.Name;
                ResourceManager rm = new ResourceManager(field.ResourceType.ToString(), typeof(Resource).Assembly);
                fieldDisplayName = rm.GetString(_labelName);
            }            
            var htmlstring = @"<label class=""" + className + @""">" + fieldDisplayName;
            if (requiredAttribute != null) { htmlstring += @"<span style=""color:red;""> *<span>"; }
            htmlstring += @"</label>";
            return new HtmlString(htmlstring); ;
        }
        private static string CreateRoutes(object routes)
        {
            if (routes == null)
                return "";
            var str = @"";
            Type t = routes.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prp in props)
            {                
                if (prp.PropertyType.IsPrimitive || prp.PropertyType == typeof(Decimal) || prp.PropertyType == typeof(String) 
                    || prp.PropertyType == typeof(DateTime)|| prp.PropertyType == typeof(DateTime?))
                {
                    object valueAsObject = prp.GetValue(routes, new object[] { });
                    var value = valueAsObject;
                    if (prp.PropertyType == typeof(DateTime) || prp.PropertyType == typeof(DateTime?))
                    {
                        value = (object)(((DateTime)valueAsObject).ToString("yyyy-MM-dd"));
                    }
                    if (prp.Name == "SortBy")
                    {                      
                        _sortBy = value == null ? null : value.ToString();
                        str += @"&" + prp.Name + @"=" + _fieldName;
                    }
                    else if (prp.Name == "OrderBy")
                    {
                        _orderBy = value == null ? null : value.ToString();
                        var orderByValue = "Asc";
                        if (_orderBy == null || _orderBy == "Desc")
                        {
                            orderByValue = "Asc";
                        }
                        else {
                            orderByValue = "Desc";
                        }
                        str += @"&" + prp.Name + @"=" + orderByValue;
                    }
                    else {
                        if (value != null)
                        {
                            str += @"&" + prp.Name + @"=" + value;
                        }                           
                    } 
                }            
            }
            return str;
        }
    }
}
