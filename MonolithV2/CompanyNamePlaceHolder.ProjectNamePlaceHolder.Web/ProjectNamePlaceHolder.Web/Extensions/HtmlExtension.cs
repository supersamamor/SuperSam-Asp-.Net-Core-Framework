using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectNamePlaceHolder.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

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
            var htmlstring = HtmlObjectCreator.TableHeaderSorterLinkHtml(fieldDisplayName, fieldName, htmlHelper.ViewData.Model, maxwidth);
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
            /// <param name="sortFieldName">Parameter name of the sorter</param>
            /// <param name="currentModelValues">Current values of the Model</param> 
            /// <param name="maxwidth"></param>
            /// <returns></returns>
            public static string TableHeaderSorterLinkHtml(string sortFieldDisplayName, string sortFieldName, object currentModelValues, int? maxwidth = null)
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
                htmlstring += @"        <a href=""?" + CreateRoutesForSorterLink(currentModelValues, sortFieldName) + @"""";
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
            
        }
    }  
}
