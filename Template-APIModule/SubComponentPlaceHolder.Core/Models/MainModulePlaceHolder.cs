namespace SubComponentPlaceHolder.Core.Models
{
    public class MainModulePlaceHolder : BaseModel
    {
        public string MainModulePlaceHolderId { get; private set; }
        public string Code { get; private set; }      
        public string Name { get; private set; }
        public void UpdateFrom(string name) 
        {
            this.Name = name;
        }
        public void SetMainModulePlaceHolderId(string mainModulePlaceHolderId)
        {
            this.MainModulePlaceHolderId = mainModulePlaceHolderId;
        }
    }
}
