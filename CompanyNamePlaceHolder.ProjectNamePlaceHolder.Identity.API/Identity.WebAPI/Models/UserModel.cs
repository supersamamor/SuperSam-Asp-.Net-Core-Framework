namespace Identity.WebAPI.Models
{
    public class UserModel
    {             
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdentityId { get; set; }
        public bool IdentityEmailConfirmed { get; set; }
        public string IdentityUserName { get; set; }
        public string IdentityEmail { get; set; }
    }
}
