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
            this.ZIndex = 1041;          
        }
      
        public PageHandler(string name, bool jqueryValidate, bool withPromptConfirmation = false, bool autoScrollUp = false)
        {
            this.Name = name;
            this.WithPromptConfirmation = withPromptConfirmation;
            this.JQueryValidate = jqueryValidate;
            this.AutomaticScrollUp = autoScrollUp;
        }

        public PageHandler(string name, bool jqueryValidate, string description)
        {
            this.Name = name;
            this.Description = description;
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

        public PageHandler(string name, bool jqueryValidate, List<string> handlerParameters)
        {
            this.Name = name;
            this.JQueryValidate = jqueryValidate;
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
        public int ZIndex { get; private set; }
        public bool AutomaticScrollUp { get; private set; }
        public string JSFunctionTriggerHandler
        {
            get
            {
                return "TriggerHandler" + this.Name;
            }
        }
        public string PromptModalName
        {
            get
            {
                return "PromptModal" + this.Name;
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
                handlerFunctionParameter = handlerFunctionParameter[1..];
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

        public IHtmlContent CelerSoftShowModalTriggerHandlerAjax(string targetElement, object currentModelValues)
        {
            var htmlstring = @"<script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            htmlstring += @"                $(""#" + targetElement + @""").append(""" + PageLoader(targetElement + "Loader", true) + @""");";
            htmlstring += @"                $('#" + targetElement + @"').load('/MainModulePlaceHolder?handler=" + this.Name + @""+ HtmlObjectCreator.CreateRoutesForListingHandler(currentModelValues) + @"', function(){ });";
            htmlstring += @"           };";
            htmlstring += @"</script>";
            return new HtmlString(htmlstring);
        }

        public IHtmlContent CelerSoftPostTriggerHandlerAjax(FormModal modal, string promptMessageContainer, string formName, string confirmationMessage = null, string runJavascriptOnSuccess = null)
        {
            var promptModalName = modal.Name + this.Name;        
            #region Trigger Post JS Function String    
            var handlerParameterQueryString = @"";
            var handlerFunctionParameter = @"";
            if (this.HandlerParameters != null)
            {               
                foreach (string param in this.HandlerParameters)
                {
                    handlerParameterQueryString += @"&" + param + (@"=' + " + param + @" +'");
                    handlerFunctionParameter += @"," + param;
                }
                handlerFunctionParameter = handlerFunctionParameter[1..];          
            }
            var postString = @"$(""#" + modal.Body + @""").append(""" + PageLoader(modal.Body + "Loader", true) + @""");" + (this.AutomaticScrollUp == true ? "document.getElementById('" + modal.Body + @"').scrollTop = 0;" : "") + "$.post('?handler=" + this.Name + handlerParameterQueryString + @"', $('#" + formName + @"').serialize(), function(data) { $('#" + modal.Body + @"').html(data); " + (runJavascriptOnSuccess ?? "") + @" });";

            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); $('#" + promptMessageContainer + @"').fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);document.getElementById('" + modal.Body + @"').scrollTop = 0;}";

            string triggerPostJSFunctionString = "";
            triggerPostJSFunctionString += @"           function " + this.JSFunctionTriggerHandler + @"(" + handlerFunctionParameter + @") {";
            if (this.WithPromptConfirmation == true)
            {
                triggerPostJSFunctionString += @"       var form = $('#" + formName + @"');";
                triggerPostJSFunctionString += @"       if ($(form).valid()) { ";
                triggerPostJSFunctionString += @"            ShowHideConfirm" + promptModalName + @"();";
                triggerPostJSFunctionString += @"       }";
                triggerPostJSFunctionString += @"       else {";
                triggerPostJSFunctionString += @"            $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); $('#" + promptMessageContainer + @"').fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);document.getElementById('" + modal.Body + @"').scrollTop = 0;";
                triggerPostJSFunctionString += @"       }";
            }
            else
            {
                if (this.JQueryValidate == true)
                {
                    triggerPostJSFunctionString += validateString;
                }
                else
                {
                    triggerPostJSFunctionString += postString;
                }
            }
            triggerPostJSFunctionString += @"           };";
            #endregion
            //Prompt Confirmation will not work if Function has parameter (HandlerParameters is not null)
            var htmlstring = this.WithPromptConfirmation ? PromptConfirmationModal(modal.Body, promptModalName, modal.ZIndex, confirmationMessage, postString) : "";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring +=               triggerPostJSFunctionString;          
            htmlstring += @"      </script>";
            return new HtmlString(htmlstring);
        }

        public IHtmlContent CelerSoftPostTriggerHandler(FormModal modal, string promptMessageContainer, string formName, string confirmationMessage = null, string runJavascriptOnSuccess = null)
        {
            var promptModalName = modal.Name + this.Name + "Prompt";
            var postString = @"$('#" + formName + @"Handler').val('" + this.Name + @"');";
            postString += (runJavascriptOnSuccess ?? "") + @"document.getElementById('" + formName + @"').submit();";

            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); }";

            var htmlstring = this.WithPromptConfirmation ? PromptConfirmationModal("", promptModalName, modal.ZIndex, confirmationMessage, postString) : "";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"$('#" + formName + @"').append('<input type=""hidden"" id=""" + formName +  @"Handler"" name=""handler"" value="""" />');";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            if (this.WithPromptConfirmation == true)
            {
                htmlstring += @"       var form = $('#" + formName + @"');";
                htmlstring += @"       if ($(form).valid()) { ";
                htmlstring += @"            ShowHideConfirm" + promptModalName + @"();";
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

        public IHtmlContent CelerSoftPostTriggerHandler(string promptMessageContainer, string formName, string confirmationMessage = null, string runJavascriptOnSuccess = null)
        {
            var promptModalName = this.Name + "Prompt";
            var postString = @"document.getElementById('" + formName + @"').submit();" + (runJavascriptOnSuccess ?? "") + @";";

            var validateString = $"var form = $('#" + formName + @"'); if ($(form).valid()) { ";
            validateString += postString + @"} else { $('#" + promptMessageContainer + @"').html('<div class=""alert alert-danger small alert-dismissible fade show"" role=""alert""><span>Please check for invalid or missing fields.</span></div>'); }";

            var htmlstring = this.WithPromptConfirmation ? PromptConfirmationModal("", promptModalName, 1, confirmationMessage, postString) : "";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"() {";
            if (this.WithPromptConfirmation == true)
            {
                htmlstring += @"       var form = $('#" + formName + @"');";
                htmlstring += @"       if ($(form).valid()) { ";
                htmlstring += @"            ShowHideConfirm" + promptModalName + @"();";
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

        public IHtmlContent CelerSoftPromptConfirmationModal(string confirmationMessage, string formName, string parameterName)
        {
            int initialZindex = this.ZIndex;
            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           $('#" + formName + @"').append('<input type=""hidden"" id=""" + formName + @"Handler"" name=""handler"" value="""" />');";
            htmlstring += @"           $('#" + formName + @"').append('<input type=""hidden"" id=""" + formName + this.Name + parameterName + @""" name=""" + parameterName + @""" value="""" />');";

            htmlstring += @"           function " + this.JSFunctionTriggerHandler + @"(" + parameterName + ") {";
            htmlstring += @"                $('#" + formName + @"Handler').val('" + this.Name + @"');";
            htmlstring += @"                $('#" + formName + this.Name + parameterName + @"').val(" + parameterName + @");";
            htmlstring += @"                $(""#" + this.PromptModalName + @""").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + this.PromptModalName + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";

            htmlstring += @"           function TriggerJS" + this.Name + @"() {";            
            htmlstring += @"                document.getElementById('" + formName + @"').submit();";
            htmlstring += @"           }";
            htmlstring += @"      </script>";
            htmlstring += @"      <div class=""modal"" id=""" + this.PromptModalName + @""" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:20%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""" + this.JSFunctionTriggerHandler + @"(0);"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + confirmationMessage + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top"" title=""Ok"" onclick=""TriggerJS" + this.Name + @"();"">";
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-placement=""top"" title=""Close"" onclick=""" + this.JSFunctionTriggerHandler + @"(0);"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + this.PromptModalName + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";       
            return new HtmlString(htmlstring);
        }

        private string PromptConfirmationModal(string modalBody, string promptModalName, int modalZIndex, string confirmationMessage, string postString)
        {           
            int initialZindex = modalZIndex + 1;           
            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function ShowHideConfirm" + promptModalName + @"() {";
            htmlstring += @"                $(""#" + promptModalName + @""").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + promptModalName + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";
            htmlstring += @"           function TriggerConfirm" + promptModalName + @"() {";
            htmlstring += @"             " + postString + ";ShowHideConfirm" + promptModalName + @"();";
            if (modalBody != null && modalBody.Trim() != "")
            {
                htmlstring +=            @"document.getElementById('" + modalBody + @"').scrollTop = 0;";
            }        
            htmlstring += @"           }";
            htmlstring += @"      </script>";
            htmlstring += @"      <div class=""modal"" id=""" + promptModalName + @""" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:20%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideConfirm" + promptModalName + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + confirmationMessage + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                          <button type=""button"" class=""btn btn-info"" data-toggle=""tooltip"" data-placement=""top"" title=""Ok"" onclick=""TriggerConfirm" + promptModalName + @"();"">";     
            htmlstring += @"                               <i class=""fas fa-check""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                          <button type=""button"" class=""btn btn-danger"" data-placement=""top"" title=""Close"" onclick=""ShowHideConfirm" + promptModalName + @"();"">";
            htmlstring += @"                               <i class=""fas fa-times-circle""></i>";
            htmlstring += @"                          </button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + promptModalName + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            return htmlstring;
        }
    }
}
