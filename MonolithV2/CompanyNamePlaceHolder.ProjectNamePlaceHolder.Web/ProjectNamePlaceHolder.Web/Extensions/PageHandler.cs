using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class PageHandler : BaseHtmlHelper
    {
        public PageHandler(string name)
        {
            this.Name = name;         
        }
        public PageHandler(string name, string description) 
        {
            this.Name = name;
            this.Description = description;
        }
        public PageHandler(string name, string description, List<string> handlerParameters)
        {
            this.Name = name;
            this.Description = description;
            this.HandlerParameters = handlerParameters;
        }
        private PageHandler()
        {
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<string> HandlerParameters { get; private set; }
        public string JSFunctionTriggerHandler
        {
            get
            {
                return "TriggerHandler" + this.Name;
            }
        }
        public IHtmlContent CelerSoftShowModalTriggerHandlerAjax(FormModal modal)
        {
            var htmlstring = @"<script type=""text/javascript"">";
            if (this.HandlerParameters == null)
            {
                htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
                htmlstring += @"               $('#" + modal.TitleHtmlElement + @"').html('" + this.Description + @"');";
                htmlstring += @"               if($('#" + modal.Name + @"Modal:visible').length == 0) {";
                htmlstring += @"                     " + modal.JSFunctionToggleShowHideModal + @"();";
                htmlstring += @"               }";
                htmlstring += @"                $('#" + modal.Body + @"').load('?handler=" + this.Name + @"', function(){ });";
                htmlstring += @"           };";
            }
            else
            {
                var handlerParameterQueryString = @"";
                var handlerFunctionParameter = @"";
                foreach (string param in this.HandlerParameters)
                {
                    handlerParameterQueryString += @"&" + param + (@"=' + " + param + @" +'");
                    handlerFunctionParameter += @"," + param;
                }
                handlerFunctionParameter = handlerFunctionParameter.Substring(1, handlerFunctionParameter.Length - 1);
                htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"(" + handlerFunctionParameter + @") {";
                htmlstring += @"               $('#" + modal.TitleHtmlElement + @"').html('" + this.Description + @"');";
                htmlstring += @"               if($('#" + modal.Name + @"Modal:visible').length == 0) {";
                htmlstring += @"                     " + modal.JSFunctionToggleShowHideModal + @"();";
                htmlstring += @"               }";
                htmlstring += @"               $('#" + modal.Body + @"').load('?handler=" + this.Name + handlerParameterQueryString + @"', function(){ });";
                htmlstring += @"           };";
            }
            htmlstring += @"</script>";
            return new HtmlString(htmlstring);
        }

        public IHtmlContent CelerSoftPostTriggerHandlerAjax(FormModal modal, string promptMessageContainer, string formName, bool validate = false, bool withConfirmation = false)
        {
            var postString = @"$.post('?handler=" + this.Name + @"', $('#" + formName + @"').serialize(), function(data) {  $('#" + modal.Body + @"').html(data); });";
            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); }";


            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            if (validate == true)
            {
                htmlstring += validateString;
            }
            else
            {
                htmlstring += postString;
            }
            htmlstring += @"           };";
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }
    }
}
