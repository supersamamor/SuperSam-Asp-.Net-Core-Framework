using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using ProjectNamePlaceHolder.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class HtmlExtension
    {
       /// <summary>
       /// Html helper to create page header with sorter
       /// </summary>
       /// <param name="expression">Model field name</param>
       /// <param name="maxwidth">Max width of the table header (value in pixel)</param>
       /// <returns></returns>
        public static IHtmlContent CelerSoftTableHeaderSorter<TProperty>(this IHtmlHelper htmlHelper, Expression<Func<object, TProperty>> expression, int? maxwidth = null)
        {
            string fieldName = "";
            var propertyGetExpression = expression.Body as MemberExpression;
            var fieldOnClosureExpression = propertyGetExpression.Expression;
            MemberInfo property = fieldOnClosureExpression.Type.GetProperty(propertyGetExpression.Member.Name);
            var field = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            string fieldDisplayName = property.Name;
            fieldName = property.Name;
            if (field != null)
            {              
                var _labelName = field.Name;
                ResourceManager rm = new ResourceManager(field.ResourceType.ToString(), typeof(Resource).Assembly);
                fieldDisplayName = rm.GetString(_labelName);
            }
            var urlHelperFactory = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var pageName = urlHelperFactory.GetUrlHelper(htmlHelper.ViewContext).ActionContext.ActionDescriptor.DisplayName.Replace("/Index", "");
            var htmlstring = HtmlObjectCreator.TableHeaderSorterLinkHtml(fieldDisplayName, pageName, fieldName, htmlHelper.ViewData.Model, maxwidth);
            return new HtmlString(htmlstring);
        }

        /// <summary>
        ///  Html helper to create pagination
        /// </summary>
        /// <param name="pagedListMetaData">List of object to be displayed as paginated list</param>
        /// <returns></returns>
        public static IHtmlContent CelerSoftTablePagination(this IHtmlHelper htmlHelper, IPagedList pagedListMetaData)
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
                htmlstring += HtmlObjectCreator.TablePageLinkHtml(htmlHelper.ViewData.Model, pageName, "<<", 1);
                htmlstring += HtmlObjectCreator.TablePageLinkHtml(htmlHelper.ViewData.Model, pageName, "Previous", previous);
            }
            for (var i = (pagedListMetaData.PageNumber - pageBuffer) <= 0 ? 1 : (pagedListMetaData.PageNumber - pageBuffer); i <= (pagedListMetaData.PageNumber + pageBuffer <= pageCount ? pagedListMetaData.PageNumber + pageBuffer : pageCount); i++)
            {
                htmlstring += HtmlObjectCreator.TablePageLinkHtml(htmlHelper.ViewData.Model, pageName, i.ToString(), i, pagedListMetaData.PageNumber);
      
            }
            if (pagedListMetaData.PageNumber != pageCount)
            {
                htmlstring += HtmlObjectCreator.TablePageLinkHtml(htmlHelper.ViewData.Model, pageName, "Next", next);
                htmlstring += HtmlObjectCreator.TablePageLinkHtml(htmlHelper.ViewData.Model, pageName, ">>", pageCount);            
            }
            htmlstring += @"</ul></ul>";
            return new HtmlString(htmlstring);
        }

        public static IHtmlContent CelerSoftFormModal(this IHtmlHelper htmlHelper, FormModal modal)
        {
            int initialZindex = 1041;
            var htmlstring = @"";
            htmlstring += @"      <div class="""" id=""" + modal.Name + @"Modal"" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:10%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style="""">";
            htmlstring += @"                     <div id=""" + modal.Name + @"Header"" class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"" id=""" + modal.TitleHtmlElement + @""">" + modal.Name + @"</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideModal" + modal.Name + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div id=""" +  modal.Body + @""" class=""modal-body"" style=""overflow-y:scroll;"">"; 
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">"; 
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div id=""" +  modal.Name + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function ShowHideModal" +  modal.Name + @"() {";
            htmlstring += @"                $(""#" +modal.Body + @""").html(""" + HtmlObjectCreator.PageLoaderString(modal.Body + "Loader", true) + @""");";
            htmlstring += @"                $(""#" +  modal.Name + @"Modal"").slideToggle(""fast"");";
            htmlstring += @"                $(""#" +  modal.Name + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";       
            htmlstring += @"           function Resize" +  modal.Name + @"() {
                                           var width = " + modal.Width + @";  
                                           var windowWidth = $(window).width(); 
                                           if (width > (windowWidth + 40)) { width = windowWidth - 40; };       
                                           var leftPosition = (windowWidth - width) / 2;   
                                           $('#" +  modal.Name + @"Modal .modal-content').width(width);
                                           $('#" +  modal.Name + @"Modal').css({ left: leftPosition }); 
                                           var windowWHeight = $(window).height(); 
                                           var maxHeight = windowWHeight - ((windowWHeight * 0.1) * 2) - 100;
                                           $('#" +  modal.Name + @"Modal  .modal-body').css({'maxHeight': maxHeight + 'px','minHeight': maxHeight + 'px','Height': maxHeight + 'px'});
                                      };";
            htmlstring += @"           function " + modal.JSFunctionToggleShowHideModal + @"() {";
            htmlstring += @"                Resize" + modal.Name + @"();ShowHideModal" + modal.Name + @"();";   
            htmlstring += @"           }";
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
            string fieldDisplayName = field.Name;
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

        public static IHtmlContent CelerSoftAjaxModalGetHandler(this IHtmlHelper htmlHelper, PageHandler handler, FormModal modal)
        {
            var htmlstring = @"<script type=""text/javascript"">";
            if (handler.HandlerParameters == null)
            {
                htmlstring += @"           function " + handler.JSFunctionTriggerHandler + @"() {";
                htmlstring += @"               $('#" + modal.TitleHtmlElement + @"').html('" + handler.Description + @"');";
                htmlstring += @"               " + modal.JSFunctionToggleShowHideModal + @"();";
                htmlstring += @"                $('#" + modal.Body + @"').load('?handler=" + handler.Name + @"', function(){ });";
                htmlstring += @"           };";
            }
            else
            {
                var handlerParameterQueryString = @"";
                var handlerFunctionParameter = @"";        
                foreach (string param in handler.HandlerParameters)
                {                             
                    handlerParameterQueryString += @"&" + param + (@"=' + " + param + @" +'");
                    handlerFunctionParameter += @"," + param;
                }
                handlerFunctionParameter = handlerFunctionParameter.Substring(1, handlerFunctionParameter.Length - 1);
                htmlstring += @"           function " + handler.JSFunctionTriggerHandler + @"(" + handlerFunctionParameter + @") {";
                htmlstring += @"               $('#" + modal.TitleHtmlElement + @"').html('" + handler.Description + @"');";
                htmlstring += @"               " + modal.JSFunctionToggleShowHideModal + @"();";
                htmlstring += @"               $('#" + modal.Body + @"').load('?handler=" + handler.Name + handlerParameterQueryString + @"', function(){ });";              
                htmlstring += @"           };";
            }
            htmlstring += @"</script>";
            return new HtmlString(htmlstring);
        }

        private static class HtmlObjectCreator
        {
            /// <summary>
            /// Creates an html for the pagination buttons/link
            /// </summary>
            /// <param name="currentModelValues">Current values of the Model</param> 
            /// <param name="pageName">Page name/Current page name</param>
            /// <param name="linkLabel">Label name of the pagination link</param>
            /// <param name="pageNo">Page Number parameter of the link</param>
            /// <param name="currentPage">Current page number parameter's value to define the active link status of the page</param>
            /// <returns></returns>
            public static string TablePageLinkHtml(object currentModelValues, string pageName, string linkLabel, int pageNo, int currentPage = 0)
            {
                var str = @"<li class=""page-item " + (pageNo == currentPage ? "active" : "") + @""">";
                str += @"<a href=""" + pageName + @"?PageNumber=" + pageNo;
                str += CreateRoutesForPaginationLink(currentModelValues) + @"""";
                str += @" class=""page-link"">" + linkLabel;
                str += @"</a>";
                str += @"</li>";
                return str;
            }

            /// <summary>
            /// Creates an html script for Table Header with Sort Functionality
            /// </summary>
            /// <param name="sortFieldDisplayName">Display name of the sorter</param>
            /// <param name="pageName">Page name/Current page name</param>
            /// <param name="sortFieldName">Parameter name of the sorter</param>
            /// <param name="currentModelValues">Current values of the Model</param> 
            /// <param name="maxwidth"></param>
            /// <returns></returns>
            public static string TableHeaderSorterLinkHtml(string sortFieldDisplayName, string pageName, string sortFieldName, object currentModelValues, int? maxwidth = null)
            {
                string currentSelectedSortBy = "";
                string currentSelectedOrderBy = "";
                GetSelectedSortByAndOrderBy(currentModelValues, out currentSelectedSortBy, out currentSelectedOrderBy);
                string maxwidthsytle = maxwidth != null ? @"style=""max-width:" + maxwidth + @"px;width:" + maxwidth + @"px;""" : "";
                var sortIcon = "fas fa-sort";
                if (currentSelectedSortBy == sortFieldName)
                {
                    sortIcon = "fas fa-sort-down";
                    if (currentSelectedOrderBy == "Asc")
                    {
                        sortIcon = "fas fa-sort-up";
                    }
                }
                var htmlstring = @"<th " + maxwidthsytle + @">";
                htmlstring += @"        <i class=""" + sortIcon + @"""></i>";
                htmlstring += @"        <a href=""" + pageName + @"?" + CreateRoutesForSorterLink(currentModelValues, sortFieldName) + @"""";
                htmlstring += @"             class=""page-sorter""> " + sortFieldDisplayName;
                htmlstring += @"        </a>";
                htmlstring += @"   </th>";
                return htmlstring;            
            }

            /// <summary>
            /// Fetch the currently selected Sort By and Order By based on current model's values
            /// </summary>
            /// <param name="currentModelValues">Current values of the Model</param> 
            /// <param name="currentSelectedSortBy">An output parameter that will return the currently selected 'Sort By' from the query string</param>
            /// <param name="currentSelectedOrderBy">An output parameter that will return the currently selected 'Order By' from the query string</param>
            private static void GetSelectedSortByAndOrderBy(object currentModelValues, out string currentSelectedSortBy, out string currentSelectedOrderBy)
            {
                currentSelectedSortBy = "";
                currentSelectedOrderBy = "";
                Type t = currentModelValues.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo prp in props.Where(l=> (new List<string> { "SortBy", "OrderBy" }).Contains(l.Name)).ToList())
                {
                    object valueAsObject = prp.GetValue(currentModelValues, new object[] { });
                    var value = valueAsObject;
                    if (prp.Name == "SortBy")
                    {
                        currentSelectedSortBy = value == null ? null : value.ToString();
                    }
                    else if (prp.Name == "OrderBy")
                    {
                        currentSelectedOrderBy = value == null ? null : value.ToString();
                    }
                }
            }

            /// <summary>
            /// Create Routes for Sort Link
            /// </summary>
            /// <param name="currentModelValues">Current values of the Model</param> 
            /// <param name="fieldName">Field name to be created (required for Page Sorter only)</param>                  
            /// <returns></returns>
            public static string CreateRoutesForSorterLink(object currentModelValues, string fieldName)
            {        
                return BaseCreateRoutes(currentModelValues, fieldName, null);
            }

            /// <summary>
            /// Create Routes for Pagination Link
            /// </summary>
            /// <param name="currentModelValues">Current values of the Model</param>         
            /// <returns></returns>
            private static string CreateRoutesForPaginationLink(object currentModelValues)
            {      
                return BaseCreateRoutes(currentModelValues, null, new List<string> { "PageNumber" });
            }

            /// <summary>
            /// Create Routes for Pagination, Sort and other links
            /// </summary>
            /// <param name="currentModelValues">Current values of the Model</param>
            /// <param name="sorterFieldName">Field name to be created (required for Page Sorter only)</param>          
            /// <param name="excludeProperties">Sets of parameter to be excluded when its route was already manually created (eg. PageNumber because it was already defined in pagination)</param>
            /// <returns></returns>
            private static string BaseCreateRoutes(object currentModelValues, string sorterFieldName, List<string> excludeProperties)
            {               
                if (excludeProperties == null) { excludeProperties = new List<string>(); }
                if (currentModelValues == null)
                    return "";
                var str = @"";
                Type t = currentModelValues.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo prp in props)
                {
                    if ((prp.PropertyType.IsPrimitive || prp.PropertyType == typeof(Decimal) || prp.PropertyType == typeof(String)
                        || prp.PropertyType == typeof(DateTime) || prp.PropertyType == typeof(DateTime?)) && !excludeProperties.Contains(prp.Name))
                    {
                        object valueAsObject = prp.GetValue(currentModelValues, new object[] { });
                        var value = valueAsObject;
                        if (prp.PropertyType == typeof(DateTime) || prp.PropertyType == typeof(DateTime?))
                        {
                            value = (object)(((DateTime)valueAsObject).ToString("yyyy-MM-dd"));
                        }
                        if (prp.Name == "SortBy")
                        {                        
                            str += @"&" + prp.Name + @"=" + (sorterFieldName == null ? value?.ToString() : sorterFieldName);
                        }
                        else if (prp.Name == "OrderBy")
                        {
                            if (sorterFieldName != null)
                            {
                                var currentSelectedOrderBy = value == null ? null : value.ToString();
                                var orderByValue = "Asc";
                                if (currentSelectedOrderBy == null || currentSelectedOrderBy == "Desc")
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

            public static string PageLoaderString(string elementId, bool isShowed = false)
            {
                return @"<style>@keyframes spin {0% {transform: rotate(0deg);} 100% { transform: rotate(360deg);}}</style><div id='" + elementId + @"' style='display:" + (isShowed == true ? "block" : "none") + @";width: 100%;height: 100%;position:absolute;background:#000;opacity:0.3;top:0;left:0;z-index:9999999;'><div style='border: 10px solid #f3f3f3;border-top: 10px solid #3498db;border-radius: 50%;width: 60px;height: 60px;animation: spin 2s linear infinite;margin: 0;top: 41%;left: 45%; -ms-transform: translate(-50%, -50%);transform: translate(-50%, -50%);position: absolute;'></div></div>";
            }
        }
    }  
}
