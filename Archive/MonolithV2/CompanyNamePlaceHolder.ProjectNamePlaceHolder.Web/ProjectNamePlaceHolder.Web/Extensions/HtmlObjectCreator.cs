using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class HtmlObjectCreator
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
            GetSelectedSortByAndOrderBy(currentModelValues, out string currentSelectedSortBy, out string currentSelectedOrderBy);
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
            foreach (PropertyInfo prp in props.Where(l => (new List<string> { "SortBy", "OrderBy" }).Contains(l.Name)).ToList())
            {
                object valueAsObject = prp.GetValue(currentModelValues, new object[] { });
                var value = valueAsObject;
                if (prp.Name == "SortBy")
                {
                    currentSelectedSortBy = value?.ToString();
                }
                else if (prp.Name == "OrderBy")
                {
                    currentSelectedOrderBy = value?.ToString();
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
          
        public static string CreateRoutesForListingHandler(object currentModelValues)
        {
            return BaseCreateRoutes(currentModelValues, null, null);
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
                        str += @"&" + prp.Name + @"=" + (sorterFieldName ?? (value?.ToString()));
                    }
                    else if (prp.Name == "OrderBy")
                    {
                        if (sorterFieldName != null)
                        {
                            var currentSelectedOrderBy = value?.ToString();
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
