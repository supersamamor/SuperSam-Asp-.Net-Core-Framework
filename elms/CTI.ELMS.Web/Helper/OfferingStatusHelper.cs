using CTI.ELMS.Core.Constants;
using MediatR;

namespace CTI.ELMS.Web.Helper
{
    public static class OfferingStatusHelper
    {        
        public static string GetOfferingStatus(string offeringStatus)
        {                 
            return offeringStatus switch
            {
                OfferingStatus.NewOS => @"<span class=""badge bg-secondary"">" + offeringStatus + "</span>",            
                OfferingStatus.CreatedAwardNotice => @"<span class=""badge bg-secondary"">" + offeringStatus + "</span>",              
                OfferingStatus.CounterOffer => @"<span class=""badge bg-primary"">" + offeringStatus + "</span>",
                OfferingStatus.RevisedOS => @"<span class=""badge bg-primary"">" + offeringStatus + "</span>",
                OfferingStatus.NewAwardNotice => @"<span class=""badge bg-secondary"">" + offeringStatus + "</span>",
                OfferingStatus.SignedOS => @"<span class=""badge bg-success"">" + offeringStatus + "</span>",
                OfferingStatus.SignedAwardNotice => @"<span class=""badge bg-success"">" + offeringStatus + "</span>",
                _ => @"<span class=""badge bg-secondary"">N/A</span>",
            };
        }
    }
}
