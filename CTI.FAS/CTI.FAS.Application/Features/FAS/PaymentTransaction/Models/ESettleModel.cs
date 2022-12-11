namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Models
{
    public class ESettleModel
    {
        public string? EntityName { get; set; } = "";
        public string? EntityAddress { get; set; } = "";
        public string? EntityTINNumber { get; set; } = "";
        public string? PayeeBankAccountNumber { get; set; } = "";
        public string? LogoFilePath { get; set; } = "";
        public string? PayeeName { get; set; } = "";
        public string? DocumentDescription { get; set; } = "";
        public decimal DocumentAmount { get; set; }
        public string? PayeeTINNumber { get; set; } = "";
        public DateTime DocumentDate { get; set; }
        public string? DocumentNumber { get; set; } = "";
        public string? ProcessingGroupLocation { get; set; } = "";
        public string? ProcessingGroupEmail { get; set; } = "";
    }
}
