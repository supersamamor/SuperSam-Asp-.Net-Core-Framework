namespace CTI.FAS.Web.Areas.FAS.Models
{
    public class PaymentTransactionTabNavigationPartial
    {
        public string TabName { get; set; } = "";
        public string? Entity { get; set; } = "";
        public void SetEntity(string? entity)
        {
            this.Entity = entity;
        }
    }
}
