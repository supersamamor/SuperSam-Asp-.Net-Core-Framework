using Microsoft.AspNetCore.Html;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class FormModal : BaseHtmlHelper
    {
        /// <summary>
        /// Create new instance of form modal with properties
        /// </summary>
        /// <param name="name">Name of the modal</param>     
        /// <param name="width">Width of the modal in pixel</param>
        public FormModal(string name, int width)
        {
            this.Name = name;        
            this.Width = width;
        }

        private FormModal()
        {
        }

        public string Name { get; private set; }        
        public int Width { get; private set; }
        public string Body 
        {
            get
            {
                return this.Name + "Body";
            }
        }
        public string JSFunctionToggleShowHideModal
        {
            get
            {
                return "ShowHide" + this.Name;
            }
        }
        public string ModalElementId
        {
            get
            {
                return this.Name + "Modal";
            }
        }
        public string TitleHtmlElement
        {
            get
            {
                return this.Name + "ModalTitle";
            }
        }
        public string PromptModalNamel
        {
            get
            {
                return this.Name + "PromptModal";
            }
        }
        
        public int ZIndex
        {
            get
            {
                return 1041;
            }
        }
        public IHtmlContent CelerSoftFormModal()
        {         
            var htmlstring = @"";
            htmlstring += @"      <div class="""" id=""" + this.ModalElementId + @""" style=""z-index: " + (ZIndex + 1) + @";position:fixed;top:10%;display:none;"">";
            htmlstring += @"           <div class=""modal-dialog"">";
            htmlstring += @"                <div class=""modal-content""  style="""">";
            htmlstring += @"                     <div id=""" + this.Name + @"Header"" class=""modal-header"">";
            htmlstring += @"                          <h6 class=""modal-title"" style=""font-weight:400;"" id=""" + this.TitleHtmlElement + @""">" + this.Name + @"</h6>";
            htmlstring += @"                          <button type=""button"" class=""close"" onclick=""ShowHideModal" + this.Name + @"();"">&times;</button>";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div id=""" + this.Body + @""" class=""modal-body"" style=""overflow-y:scroll;"">";
            htmlstring += @"                     </div>";
            htmlstring += @"                     <div class=""modal-footer"">";
            htmlstring += @"                     </div>";
            htmlstring += @"                </div>";
            htmlstring += @"           </div>";
            htmlstring += @"      </div>";

            htmlstring += @"      <div id=""" + this.Name + @"BackGround"" style=""display:none;position:fixed;top:0;left:0;z-index:" + ZIndex + @";width:100vw;height:100vh;background-color:#000;opacity:0.3;""></div>";
           
            htmlstring += @"      <script type=""text/javascript"">";
            htmlstring += @"           function ShowHideModal" + this.Name + @"() {";
            htmlstring += @"                $(""#" + this.Body + @""").html(""" + PageLoader(this.Body + "Loader", true) + @""");";
            htmlstring += @"                $(""#" + this.ModalElementId + @""").slideToggle(""fast"");";
            htmlstring += @"                $(""#" + this.Name + @"BackGround"").slideToggle(""fast"");";
            htmlstring += @"           }";
            htmlstring += @"           function Resize" + this.Name + @"() {
                                           var width = " + this.Width + @";  
                                           var windowWidth = $(window).width(); 
                                           if (width > (windowWidth + 40)) { width = windowWidth - 40; };       
                                           var leftPosition = (windowWidth - width) / 2;   
                                           $('#" + this.ModalElementId + @" .modal-content').width(width);
                                           $('#" + this.ModalElementId + @"').css({ left: leftPosition }); 
                                           var windowWHeight = $(window).height(); 
                                           var maxHeight = windowWHeight - ((windowWHeight * 0.1) * 2) - 100;
                                           $('#" + this.ModalElementId + @"  .modal-body').css({'maxHeight': maxHeight + 'px','minHeight': maxHeight + 'px','Height': maxHeight + 'px'});
                                      };";
            htmlstring += @"           function " + this.JSFunctionToggleShowHideModal + @"() {";
            htmlstring += @"                Resize" + this.Name + @"();ShowHideModal" + this.Name + @"();";
            htmlstring += @"           }";
            htmlstring += @"      </script>";

            return new HtmlString(htmlstring);
        }      
    }
}
