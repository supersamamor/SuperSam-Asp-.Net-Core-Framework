using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using ProjectNamePlaceHolder.Application;
using System;
using System.ComponentModel.DataAnnotations;
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
            var propertyGetExpression = expression.Body as MemberExpression;
            var fieldOnClosureExpression = propertyGetExpression.Expression;
            MemberInfo property = fieldOnClosureExpression.Type.GetProperty(propertyGetExpression.Member.Name);
            string fieldDisplayName = property.Name;
            string fieldName = property.Name;
            if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute field)
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
              
        public static IHtmlContent DisplayLabelWithRequiredTag<TProperty>(this IHtmlHelper htmlHelper, Expression<Func<object, TProperty>> expression, string className = null)
        {
            var propertyGetExpression = expression.Body as MemberExpression;
            var fieldOnClosureExpression = propertyGetExpression.Expression;
            MemberInfo property = fieldOnClosureExpression.Type.GetProperty(propertyGetExpression.Member.Name);
            var field = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            string fieldDisplayName = field.Name;
            if (field != null)
            {
                var _labelName = field.Name;
                ResourceManager rm = new ResourceManager(field.ResourceType.ToString(), typeof(Resource).Assembly);
                fieldDisplayName = rm.GetString(_labelName);
            }
            var htmlstring = @"<label class=""" + className + @""">" + fieldDisplayName;
            if (property.GetCustomAttribute(typeof(RequiredAttribute)) is RequiredAttribute) { htmlstring += @"<span style=""color:red;""> *<span>"; }
            htmlstring += @"</label>";
            return new HtmlString(htmlstring); ;
        }       
    }  
}
