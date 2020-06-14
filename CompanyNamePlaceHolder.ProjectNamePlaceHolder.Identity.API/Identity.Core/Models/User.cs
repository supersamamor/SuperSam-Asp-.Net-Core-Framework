namespace Identity.Core.Models
{
    public class User : BaseModel
    {   
        public string FullName { get; private set; }
        public string IdentityId { get; private set; }
        public bool IdentityEmailConfirmed { get; private set; }
        public string IdentityUserName { get; private set; }
        public string IdentityEmail { get; private set; }
        public void UpdateFrom(string fullName) {
            this.FullName = fullName;
        }
    }
}
