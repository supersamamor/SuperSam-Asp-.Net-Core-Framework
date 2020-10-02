using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class PromptModal
    {
        public PromptModal(string name = null, int width = 0)
        {
            this.Name = name;
            this.Width = width;
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int ZIndex
        {
            get
            {
                return 1041;
            }
        }

        public string JSFunctionToggleShowHideModal
        {
            get
            {
                return "ShowHide" + this.Name;
            }
        }

        public IHtmlContent CelerSoftPromptConfirmationMultipleHandlerModal(string confirmationMessage, IList<PageHandler> handlerList, List<ButtonStyle> buttonCssClass)
        {
            string jsFunctions = @"";
            string buttonsString = @"";
            int counter = 0;
            foreach (var handler in handlerList)
            {
                jsFunctions += @"           function TriggerConfirm" + this.Name + handler.Name + @"() {";
                jsFunctions += @"             " + handler.JSFunctionTriggerHandler + @"();";
                jsFunctions += @"           }";

                buttonsString += @"                          <button type=""button"" class=""" + buttonCssClass[counter].ButtonClass + @""" data-toggle=""tooltip"" data-placement=""top"" title=""" + handler.Description + @""" onclick=""TriggerConfirm" + this.Name + handler.Name + @"();"">";
                buttonsString += @"                               <i class=""" + buttonCssClass[counter].IconClass + @"""></i>";
                buttonsString += @"                          </button>";
                counter = counter + 1;
            }

            int initialZindex = ZIndex + 1;
            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionToggleShowHideModal + @"() {";
            htmlstring += @"                $(""#" + this.Name + @""").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + this.Name + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";

            htmlstring += jsFunctions;
            
            htmlstring += @"      </script>";
            htmlstring += @"      <div class=""modal"" id=""" + this.Name + @""" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:20%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"">Confirmation</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideConfirm" + this.Name + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + confirmationMessage + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";

            htmlstring += buttonsString;

            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + this.Name + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            return new HtmlString(htmlstring);
        }
        public IHtmlContent CelerSoftMessageModal(string message)
        {
            int initialZindex = ZIndex + 1;
            var htmlstring = @"";
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function " + this.JSFunctionToggleShowHideModal + @"() {";
            htmlstring += @"                $(""#" + this.Name + @""").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + this.Name + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";
            htmlstring += @"      </script>";
            htmlstring += @"      <div class=""modal"" id=""" + this.Name + @""" style=""z-index: " + (initialZindex + 1) + @";position:fixed;top:20%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"" style=""" + (this.Width > 0 ? "width:" + this.Width + "px;max-width:" + this.Width + "px;" : "") + @""">";
            htmlstring += @"                <div class=""modal-content""  style=""z-index: " + (initialZindex + 1) + @";"">";
            htmlstring += @"                     <div class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;""></h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""" + this.JSFunctionToggleShowHideModal + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-body"">";
            htmlstring += @"                          " + message + @"   ";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";
            htmlstring += @"      <div  id=""" + this.Name + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + initialZindex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
            return new HtmlString(htmlstring);
        }

    }
}
