using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
namespace Template.Web.Extensions
{
    public static class HtmlExtension
    {
        private static string _sortBy { get; set; }
        private static string _orderBy { get; set; }
        private static string _fieldName { get; set; }
        private static string _pageName { get; set; }
        public static IHtmlContent PageSorter<TProperty>(this IHtmlHelper htmlHelper, Expression<Func<object, TProperty>> expression)
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
                ResourceManager rm = new ResourceManager(field.ResourceType.ToString(), Assembly.GetExecutingAssembly());
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
            var htmlstring = @"<th>";
            htmlstring += @"<i class=""" + sortIcon + @"""></i>";
            htmlstring += @"<a href=""" + _pageName + @"?";
            htmlstring += @"" + routes + @"""";
            htmlstring += @"class=""page-sorter""> " + fieldDisplayName + "</a>";
            htmlstring += @"</th>";
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
                    || prp.PropertyType == typeof(DateTime))
                {
                    object value = prp.GetValue(routes, new object[] { });                   
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
