namespace CTI.ContractManagement.EmailSending
{
    public class MailSettings
    {
        public string? EmailApiUrl { get; set; }
        public string? EmailApiUsername { get; set; }
        public string? EmailApiPassword { get; set; }
        public string? EmailApiSender { get; set; }
        public string? SMTPEmail { get; set; }
        public string? SMTPEmailPassword { get; set; }
        public string? SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string? SendingType { get; set; }
    }
}
