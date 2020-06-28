namespace SubComponentPlaceHolder.Data.Models
{
    public class SubComponentPlaceHolderApiClient : BaseEntity
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Secret { get; set; }
        public bool Active { get; set; }
    }
}
