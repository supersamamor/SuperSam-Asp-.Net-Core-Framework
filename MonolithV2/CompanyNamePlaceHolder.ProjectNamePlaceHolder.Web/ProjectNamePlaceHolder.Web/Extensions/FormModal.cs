namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class FormModal
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
        public string TitleHtmlElement
        {
            get
            {
                return this.Name + "ModalTitle";
            }
        }
    }
}
