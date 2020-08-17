using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using ProjectNamePlaceHolder.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class FormModalProperties
    {
        public string ModalElementId { get; set; }
        public string HandlerName { get; set; }
        public string TriggerShowJsFunction { get; set; }
        public string ModalTitle { get; set; }
        public int ModalWidth { get; set; }
    }
    public class ConfirmationModalProperties
    {
        public string HandlerName { get; set; }
        public string ModalElementId { get; set; }
        public string TriggerShowElementId { get; set; }
        public string TriggerFormActionElementId { get; set; }
        public string FormId { get; set; }
        public string PromptMessageContainerId { get; set; }
        public string ResultModalId { get; set; }
        public string Message { get; set; }
    }

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
            string routes = "";
            routes = CreateRoutes(htmlHelper.ViewData.Model, true);
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
            htmlstring += @"        <a href=""" + _pageName + @"?" + routes + @""""; 
            htmlstring += @"             class=""page-sorter""> " + fieldDisplayName;
            htmlstring += @"        </a>";
            htmlstring += @"   </th>";
            return new HtmlString(htmlstring);
        }
        public static IHtmlContent PromptConfirmationModal(this IHtmlHelper htmlHelper, string modalId, string triggerShowElementId, string triggerActionElement,
              string formId, string promptMessageContainerId, string message)
        {
            int initialZindex = 1040;
            var htmlstring = @"";
            htmlstring += @"      <style> ";
            htmlstring += @"           @keyframes spin {";
            htmlstring += @"                0% {transform: rotate(0deg);}";
            htmlstring += @"                100% { transform: rotate(360deg);}";
            htmlstring += @"           }";
            htmlstring += @"      </style>";       
            htmlstring += @"      ";
            htmlstring += @"      <div id=""" + modalId + @"_Loader"" style=""display:none;width: 100%;height: 100%;position: fixed;z-index: 9999;background:#000;opacity:0.3;top:0px;left:0px;"">";
            htmlstring += @"           <div style=""border:10px solid #f3f3f3;border-top: 10px solid #3498db;border-radius:50%;width:60px;height:60px;animation:spin 2s linear infinite;margin:0;top:41%;left:45%;-ms-transform:translate(-50%, -50%);transform:translate(-50%, -50%);position:absolute;""></div>";
            htmlstring += @"      </div>";
            htmlstring += @"      ";
            htmlstring += @"      <div class=""modal"" id=""" + modalId + @""" style=""position:fixed;top:20%;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideModal" + modalId + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + message + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top""";
            htmlstring += @"                               title=""Ok"" onclick=""$('#" + modalId + @"_Loader').show();ShowHideModal" + modalId + @"();$('#" + triggerActionElement + @"').click();"">";
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-placement=""top"" title=""Close"" onclick="" ShowHideModal" + modalId + @"();"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + modalId + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            htmlstring += @"      <script type=""text/javascript"">";
            if (formId == "")
            {
                htmlstring += @"       function ShowHideModal" + modalId + @"() {";
                htmlstring += @"            Resize" + modalId + @"();";
                htmlstring += @"            $(""#" + modalId + @"BackGround"").slideToggle(""fast"");";
                htmlstring += @"            $(""#" + modalId + @""").slideToggle(""fast"");";
                htmlstring += @"       }";
            }
            else
            {
                htmlstring += @"       function ShowHideModal" + modalId + @"() {";
                htmlstring += @"            Resize" + modalId + @"();";
                htmlstring += @"            var form = $('#" + formId + @"');";
                htmlstring += @"            if ($(form).valid()) {";
                htmlstring += @"                 $(""#" + modalId + @"BackGround"").slideToggle(""fast"");";
                htmlstring += @"                 $(""#" + modalId + @""").slideToggle(""fast"");";
                htmlstring += @"            }";
                htmlstring += @"            else";
                htmlstring += @"            {";
                htmlstring += @"                 $('#" + promptMessageContainerId + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>');";
                htmlstring += @"            }";
                htmlstring += @"       }";
            }
            htmlstring += @"           $( ""#" + triggerShowElementId + @""" ).bind( ""click"", function() {";
            htmlstring += @"                ShowHideModal" + modalId + @"();";
            htmlstring += @"           });";
            htmlstring += @"           ";
            htmlstring += @"           $(document).ready(function() {";
            htmlstring += @"                $(window).keydown(function(event){";
            htmlstring += @"                     if(event.keyCode == 13 && !event.shiftKey) {";
            htmlstring += @"                          event.preventDefault();";
            htmlstring += @"                          ShowHideModal" + modalId + @"();";
            htmlstring += @"                          return false;";
            htmlstring += @"                     }";
            htmlstring += @"                });";
            htmlstring += @"           });";
            htmlstring += @"           function Resize" + modalId + @"() {
                                           var width = 500;   
                                           var windowWidth = $(window).width(); 
                                           if (width > (windowWidth + 40)) { width = windowWidth - 40; };       
                                           var leftPosition = (windowWidth - width) / 2;   
                                           $('#" + modalId + @" .modal-content').width(width);                            
                                           var windowWHeight = $(window).height(); 
                                           var maxHeight = windowWHeight - ((windowWHeight * 0.2) * 2) - 100;
                                           $('#" + modalId + @"  .modal-body').css({'maxHeight': maxHeight + 'px'});
                                      }";
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }
        public static IHtmlContent PromptConfirmationModalAjax(this IHtmlHelper htmlHelper, ConfirmationModalProperties properties)
        {
            int initialZindex = 1040;
            var htmlstring = @"";
            htmlstring += @"      <style> ";
            htmlstring += @"           @keyframes spin {";
            htmlstring += @"                0% {transform: rotate(0deg);}";
            htmlstring += @"                100% { transform: rotate(360deg);}";
            htmlstring += @"           }";
            htmlstring += @"      </style>";
            htmlstring += @"      ";
            htmlstring += @"      <div class=""modal"" id=""" + properties.ModalElementId + @""" style=""position:fixed;top:20%;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideModal" + properties.ModalElementId + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + properties.Message + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top""";
            htmlstring += @"                               title=""Ok"" onclick=""ShowHideModal" + properties.ModalElementId + @"();$('#" + properties.TriggerFormActionElementId + @"').click();"">";
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-placement=""top"" title=""Close"" onclick="" ShowHideModal" + properties.ModalElementId + @"();"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + properties.ModalElementId + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            htmlstring += @"      <script type=""text/javascript"">";
            if (properties.FormId == "")
            {
                htmlstring += @"       function ShowHideModal" + properties.ModalElementId + @"() {";
                htmlstring += @"            Resize" + properties.ModalElementId + @"();";
                htmlstring += @"            $(""#" + properties.ModalElementId + @"BackGround"").slideToggle(""fast"");";
                htmlstring += @"            $(""#" + properties.ModalElementId + @""").slideToggle(""fast"");";
                htmlstring += @"       }";
            }
            else
            {
                htmlstring += @"       function ShowHideModal" + properties.ModalElementId + @"() {";
                htmlstring += @"            Resize" + properties.ModalElementId + @"();";
                htmlstring += @"            var form = $('#" + properties.FormId + @"');";
                htmlstring += @"            if ($(form).valid()) {";
                htmlstring += @"                 $(""#" + properties.ModalElementId + @"BackGround"").slideToggle(""fast"");";
                htmlstring += @"                 $(""#" + properties.ModalElementId + @""").slideToggle(""fast"");";
                htmlstring += @"            }";
                htmlstring += @"            else";
                htmlstring += @"            {";
                htmlstring += @"                 $('#" + properties.PromptMessageContainerId + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>');";
                htmlstring += @"            }";
                htmlstring += @"       }";
            }
            htmlstring += @"           $( ""#" + properties.TriggerShowElementId + @""" ).bind( ""click"", function() {";
            htmlstring += @"                ShowHideModal" + properties.ModalElementId + @"();";
            htmlstring += @"           });";
            htmlstring += @"           ";
            htmlstring += @"           $(document).ready(function() {";
            htmlstring += @"                $(window).keydown(function(event){";
            htmlstring += @"                     if(event.keyCode == 13 && !event.shiftKey) {";
            htmlstring += @"                          event.preventDefault();";
            htmlstring += @"                          ShowHideModal" + properties.ModalElementId + @"();";
            htmlstring += @"                          return false;";
            htmlstring += @"                     }";
            htmlstring += @"                });";
            htmlstring += @"           });";
            htmlstring += @"           function Resize" + properties.ModalElementId + @"() {
                                           var width = 500;   
                                           var windowWidth = $(window).width(); 
                                           if (width > (windowWidth + 40)) { width = windowWidth - 40; };       
                                           var leftPosition = (windowWidth - width) / 2;   
                                           $('#" + properties.ModalElementId + @" .modal-content').width(width);                            
                                           var windowWHeight = $(window).height(); 
                                           var maxHeight = windowWHeight - ((windowWHeight * 0.2) * 2) - 100;
                                           $('#" + properties.ModalElementId + @"  .modal-body').css({'maxHeight': maxHeight + 'px'});
                                      }";

            htmlstring += @"          $('#" + properties.TriggerFormActionElementId + @"').on('click', function (evt) {
                                            evt.preventDefault();
                                            $.post('?handler=" + properties.HandlerName + @"', $('#" + properties.FormId + @"').serialize(), function (data) {
                                                $('#" + properties.ResultModalId + @"Body').html(data);
                                            });
                                      });";

            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        } 

        public static IHtmlContent FormModal(this IHtmlHelper htmlHelper, FormModalProperties properties, object handlerParameters = null)
        {          
            int initialZindex = 1041;
            var htmlstring = @"";  
            htmlstring += @"      <div class="""" id=""" + properties.ModalElementId + @"Modal"" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:10%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style="""">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">" + properties.ModalTitle + @"</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideModal" + properties.ModalElementId + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div id=""" + properties.ModalElementId + @"Body"" class=""modal-body"" style=""overflow-y:scroll;"">";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div id=""" + properties.ModalElementId + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function ShowHideModal" + properties.ModalElementId + @"() {";
            htmlstring += @"                $(""#" + properties.ModalElementId + @"Modal"").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + properties.ModalElementId + @"BackGround"").slideToggle(""fast"");";    
            htmlstring += @"           }";

            htmlstring +=         CreateJSTriggerMethod(properties.TriggerShowJsFunction, properties.ModalElementId, properties.HandlerName, handlerParameters);

            htmlstring += @"           function Resize" + properties.ModalElementId + @"() {
                                           var width = " + properties.ModalWidth + @";  
                                           var windowWidth = $(window).width(); 
                                           if (width > (windowWidth + 40)) { width = windowWidth - 40; };       
                                           var leftPosition = (windowWidth - width) / 2;   
                                           $('#" + properties.ModalElementId + @"Modal .modal-content').width(width);
                                           $('#" + properties.ModalElementId + @"Modal').css({ left: leftPosition }); 
                                           var windowWHeight = $(window).height(); 
                                           var maxHeight = windowWHeight - ((windowWHeight * 0.1) * 2) - 100;
                                           $('#" + properties.ModalElementId + @"Modal  .modal-body').css({'maxHeight': maxHeight + 'px'});
                                      }";
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

        public static IHtmlContent CelerSoftPager(this IHtmlHelper htmlHelper, IPagedList pagedListMetaData)
        {
            var pageBuffer = 5;
            var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var pageName = urlHelperFactory.GetUrlHelper(htmlHelper.ViewContext).ActionContext.ActionDescriptor.DisplayName.Replace("/Index", "");

            var pageCount = pagedListMetaData.PageSize != 0 ? (pagedListMetaData.TotalItemCount / pagedListMetaData.PageSize) + 1 : 0;
            var previous = pagedListMetaData.PageNumber - pageBuffer <= 0 ? 1 : pagedListMetaData.PageNumber - pageBuffer;
            var next = pagedListMetaData.PageNumber + pageBuffer >= pageCount ? pageCount : pagedListMetaData.PageNumber + pageBuffer;
            var htmlstring = @"<ul class=""pagination justify-content-center"">";
            htmlstring += @"<ul class=""pagination pagination-sm no-margin pull-right"">";
            if (pagedListMetaData.PageNumber != 1)
            {
                htmlstring += CreatePagelLink(htmlHelper.ViewData.Model, pageName, "<<", 1);
                htmlstring += CreatePagelLink(htmlHelper.ViewData.Model, pageName, "Previous", previous);
            }
            for (var i = (pagedListMetaData.PageNumber - pageBuffer) <= 0 ? 1 : (pagedListMetaData.PageNumber - pageBuffer); i <= (pagedListMetaData.PageNumber + pageBuffer <= pageCount ? pagedListMetaData.PageNumber + pageBuffer : pageCount); i++)
            {
                htmlstring += CreatePagelLink(htmlHelper.ViewData.Model, pageName, i.ToString(), i, pagedListMetaData.PageNumber);
            }
            if (pagedListMetaData.PageNumber != pageCount)
            {
                htmlstring += CreatePagelLink(htmlHelper.ViewData.Model, pageName, "Next", next);
                htmlstring += CreatePagelLink(htmlHelper.ViewData.Model, pageName, ">>", pageCount);
            }
            htmlstring += @"</ul></ul>";
            return new HtmlString(htmlstring);
        }

        private static string CreatePagelLink(object routes, string pageName, string label, int pageNo, int currentPage = 0)
        {
            var str = @"<li class=""page-item " + (label == currentPage.ToString() ? "active" : "") + @""">";
            str += @"<a href=""" + pageName + @"?PageNumber=" + pageNo;
            str += CreateRoutes(routes, false, new List<string> { "PageNumber" }) + @"""";
            str += @" class=""page-link"">" + label;
            str += @"</a>";
            str += @"</li>";
            return str;
        }
        
        private static string CreateJSTriggerMethod(string triggerShowJsFunction, string modalElementId, string handlerName, object handlerParameters)
        {
            var htmlstring = @"";
            if (handlerParameters == null)
            {
                htmlstring += @"           function " + triggerShowJsFunction + @"() {";
                htmlstring += @"                $('#" + modalElementId + @"Body').load('?handler=" + handlerName + @"', function(){ Resize" + modalElementId + @"();});";
                htmlstring += @"                ShowHideModal" + modalElementId + @"();";
                htmlstring += @"           };";
            }
            else
            {
                var handlerParameterQueryString = @"";
                var handlerFunctionParameter = @"";
                Type t = handlerParameters.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo prp in props)
                {
                    object valueAsObject = prp.GetValue(handlerParameters, new object[] { });
                    var value = valueAsObject;
                    string paramValue = value == null || value?.ToString() == "" ? @"=' + " + prp.Name + @" +'" : @"=" + value.ToString() + @"";
                    handlerParameterQueryString += @"&" + prp.Name + paramValue;
                    handlerFunctionParameter += @"," + prp.Name;
                }
                handlerFunctionParameter = handlerFunctionParameter.Substring(1, handlerFunctionParameter.Length - 1);
                htmlstring += @"           function "+ triggerShowJsFunction + @"(" + handlerFunctionParameter + @") {";
                htmlstring += @"                $('#" + modalElementId + @"Body').load('?handler=" + handlerName + handlerParameterQueryString + @"', function(){ Resize" + modalElementId + @"();});";
                htmlstring += @"                ShowHideModal" + modalElementId + @"();";
                htmlstring += @"           };";
            }        
            return htmlstring;
        }

        private static string CreateRoutes(object routes, bool isSorter, List<string> excludeProperties = null)
        {
            if (excludeProperties == null) { excludeProperties = new List<string>(); }
            if (routes == null)
                return "";
            var str = @"";
            Type t = routes.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prp in props)
            {           
                if ((prp.PropertyType.IsPrimitive || prp.PropertyType == typeof(Decimal) || prp.PropertyType == typeof(String) 
                    || prp.PropertyType == typeof(DateTime)|| prp.PropertyType == typeof(DateTime?)) && !excludeProperties.Contains(prp.Name))
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
                        if (isSorter == true)
                        {
                            _orderBy = value == null ? null : value.ToString();
                            var orderByValue = "Asc";
                            if (_orderBy == null || _orderBy == "Desc")
                            {
                                orderByValue = "Asc";
                            }
                            else
                            {
                                orderByValue = "Desc";
                            }
                            str += @"&" + prp.Name + @"=" + orderByValue;
                        }
                        else
                        {
                            str += @"&" + prp.Name + @"=" + value;
                        }                      
                    }
                    else
                    {
                        if (value != null && value.ToString() != "")
                        {
                            str += @"&" + prp.Name + @"=" + value;
                        }
                        else
                        {
                            str += @"";
                        }
                    }                                     
                }            
            }
            return str;
        }
    }
}
