namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class PromptContainerMessageTempDataName
    {
        public const string Error = "Error";
        public const string Success = "Success";
    }

    public class PromptContainer
    {
        public PromptContainer(string name, string effects = null)
        {
            this.Name = name;
            this.Effects = effects;
        }
        private PromptContainer(){}
        public string Name { get; set; }
        public string Effects { get; set; }
    }
}
