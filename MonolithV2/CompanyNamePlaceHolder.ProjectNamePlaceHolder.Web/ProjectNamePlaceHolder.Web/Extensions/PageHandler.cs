using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class PageHandler
    {
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
    }
}
