using CTI.DPI.Application.Features.DPI.Approval.Queries;
using CTI.DPI.Core.DPI;
using MediatR;

namespace CTI.DPI.Web.Helper
{
    public static class DisplayOnDashboardHelper
    {
        public static string GetBadge(bool isActive)
        {
            switch (isActive)
            {
                case true:
                    return @"<span class=""badge bg-success"">Show</span>";
                case false:
                    return @"<span class=""badge bg-secondary"">Hide</span>";
                default:
            }
        }
    }
}
