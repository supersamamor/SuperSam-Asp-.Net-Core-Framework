using CTI.FAS.Core.Constants;
using MediatR;

namespace CTI.FAS.Web.Helper
{  
    public static class PaymentStatusHelper
    {
        public static string GetPaymentStatus(string status)
        {
            switch (status)
            {
                case PaymentTransactionStatus.New:
                    return @"<span class=""badge bg-primary"">" + status + "</span>";
                case PaymentTransactionStatus.Generated:
                    return @"<span class=""badge bg-info"">" + status + "</span>";
                case PaymentTransactionStatus.Sent:
                    return @"<span class=""badge bg-success"">" + status + "</span>";
                case PaymentTransactionStatus.Revoked:
                    return @"<span class=""badge bg-warning"">" + status + "</span>";
                default:
                    break;
            }
            return @"<span class=""badge bg-secondary"">" + status + "</span>";
        }
    }
}
