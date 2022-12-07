using CTI.FAS.Core.Constants;

namespace CTI.FAS.Web.Helper
{
    public static class EnrollmentStatusHelper
    {
        public static string GetEnrollmentStatus(string? status)
        {        
            switch (status)
            {
                case EnrollmentStatus.New:
                    return @"<span class=""badge bg-info"">" + status + "</span>";
                case EnrollmentStatus.InActive:
                    return @"<span class=""badge bg-warning"">" + status + "</span>";
                case EnrollmentStatus.Active:
                    return @"<span class=""badge bg-success"">" + status + "</span>";
                case EnrollmentStatus.ForReEnrollment:
                    return @"<span class=""badge bg-info"">" + status + "</span>";              
                default:
                    break;
            }
            return @"<span class=""badge bg-secondary"">" + status + "</span>";
        }
    }
}
