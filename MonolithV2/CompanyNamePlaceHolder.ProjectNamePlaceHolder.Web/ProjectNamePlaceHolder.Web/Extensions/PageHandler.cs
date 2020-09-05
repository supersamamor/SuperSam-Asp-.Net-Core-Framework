using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class PageHandler : BaseHtmlHelper
    {
        public PageHandler(string name, bool withPromptConfirmation = false)
        {
            this.Name = name;  
            this.WithPromptConfirmation = withPromptConfirmation;   
        }

        public PageHandler(string name, bool jqueryValidate, bool withPromptConfirmation = false)
        {
            this.Name = name;
            this.WithPromptConfirmation = withPromptConfirmation;
            this.JQueryValidate = jqueryValidate;            
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
        public bool JQueryValidate { get; private set; }
        public bool WithPromptConfirmation { get; private set; }

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

        public IHtmlContent CelerSoftPostTriggerHandlerAjax(FormModal modal, string promptMessageContainer, string formName, string confirmationMessage = null, string runJavascriptOnSuccess = null)
        {
            var postString = @"$(""#" + modal.Body + @""").append(""" + PageLoader(modal.Body + "Loader", true) + @""");$.post('?handler=" + this.Name + @"', $('#" + formName + @"').serialize(), function(data) { $('#" + modal.Body + @"').html(data); " + (runJavascriptOnSuccess != null ? runJavascriptOnSuccess : "") + @" });";

            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); }";
                   
            var htmlstring = this.WithPromptConfirmation ? PromptConfirmationModal(modal, confirmationMessage, postString) : "";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            if (this.WithPromptConfirmation == true)
            {
                htmlstring += @"       var form = $('#" + formName + @"');";
                htmlstring += @"       if ($(form).valid()) { ";
                htmlstring += @"            ShowHideConfirm" + modal.Name + @"();";  
                htmlstring += @"       }";
                htmlstring += @"       else {";
                htmlstring += @"            $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); $('#" + promptMessageContainer + @"').fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);";
                htmlstring += @"       }";
            }
            else 
            {
                if (this.JQueryValidate == true)
                {
                    htmlstring += validateString;
                }
                else
                {
                    htmlstring += postString;
                }
            }  
            htmlstring += @"           };";
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }
        public IHtmlContent CelerSoftPostTriggerHandler(FormModal modal, string promptMessageContainer, string formName, string confirmationMessage = null, string runJavascriptOnSuccess = null)
        {
            var postString = @"document.getElementById('"+ formName + @"').submit();" + (runJavascriptOnSuccess != null ? runJavascriptOnSuccess : "") + @";";

            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); }";

            var htmlstring = this.WithPromptConfirmation ? PromptConfirmationModal(modal, confirmationMessage, postString) : "";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            if (this.WithPromptConfirmation == true)
            {
                htmlstring += @"       var form = $('#" + formName + @"');";
                htmlstring += @"       if ($(form).valid()) { ";
                htmlstring += @"            ShowHideConfirm" + modal.Name + @"();";
                htmlstring += @"       }";
                htmlstring += @"       else {";
                htmlstring += @"            $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); ";
                htmlstring += @"       }";
            }
            else
            {
                if (this.JQueryValidate == true)
                {
                    htmlstring += validateString;
                }
                else
                {
                    htmlstring += postString;
                }
            }
            htmlstring += @"           };";
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }

        private string PromptConfirmationModal(FormModal modal, string confirmationMessage, string postString)
        {
            int initialZindex = modal.ZIndex + 1;           
            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function ShowHideConfirm" + modal.Name + @"() {";
            htmlstring += @"                $(""#" + modal.Name + @"PromptModal"").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + modal.Name + @"PromptModalBackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";
            htmlstring += @"           function TriggerConfirm" + modal.Name + @"() {";
            htmlstring += @"               " + postString;
            htmlstring += @"           }";
            htmlstring += @"      </script>";
            htmlstring += @"      <div class=""modal"" id=""" + modal.Name + @"PromptModal"" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:20%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideConfirm" + modal.Name + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + confirmationMessage + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top"" title=""Ok"" onclick=""TriggerConfirm" + modal.Name + @"();"">";     
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-placement=""top"" title=""Close"" onclick=""ShowHideConfirm" + modal.Name + @"();"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + modal.Name + @"PromptModalBackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            return htmlstring;
        }
    }
}
