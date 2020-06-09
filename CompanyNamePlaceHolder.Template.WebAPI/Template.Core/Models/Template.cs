namespace Template.Core.Models
{
    public class Template : BaseModel
    {    
        public string Code { get; private set; }      
        public string Name { get; private set; }
        public void UpdateFrom(string name) {
            this.Name = name;
        }
    }
}
