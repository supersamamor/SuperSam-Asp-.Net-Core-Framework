namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class ButtonStyle
    {
        public ButtonStyle(string buttonClass, string iconClass)
        {
            this.ButtonClass = buttonClass;
            this.IconClass = iconClass;
        }
        public string ButtonClass { get; set; }
        public string IconClass { get; set; }
    }
}
