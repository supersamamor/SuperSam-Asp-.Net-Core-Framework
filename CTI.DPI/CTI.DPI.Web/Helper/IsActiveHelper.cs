using CTI.DPI.Application.Features.DPI.Approval.Queries;
using CTI.DPI.Core.DPI;
using MediatR;

namespace CTI.DPI.Web.Helper
{
    public static class IsActiveHelper
    {
        public static string GetStatus(bool isActive)
        {
            switch (isActive)
            {
                case true:
                    return @"<span class=""badge bg-success"">Active</span>";
                case false:
                    return @"<span class=""badge bg-secondary"">In-Active</span>";
                default:
            }
        }
    }
}
