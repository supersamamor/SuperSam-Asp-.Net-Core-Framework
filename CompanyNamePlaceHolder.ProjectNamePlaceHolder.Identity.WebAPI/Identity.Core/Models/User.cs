namespace Identity.Core.Models
{
    public class User : BaseModel
    {   
        public string FullName { get; private set; }
        public void UpdateFrom(string fullName) {
            this.FullName = fullName;
        }
    }
}
