using CTI.ELMS.Application.Features.ELMS.TabNavigation.Models;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries
{
    public record GetTabNavigationByActivityIdQuery(string ActivityId, string TabName) : IRequest<TabNavigationModel>;
   
    public class GetTabNavigationByActivityIdQueryHandler : IRequestHandler<GetTabNavigationByActivityIdQuery, TabNavigationModel>
    {
        private readonly ApplicationContext _context;
        public GetTabNavigationByActivityIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TabNavigationModel> Handle(GetTabNavigationByActivityIdQuery request, CancellationToken cancellationToken)
        {
            return await (from a in _context.Lead.Include(l => l.OfferingList)
                          join b in _context.Activity on a.Id equals b.LeadID
                          where b.Id == request.ActivityId
                          select new TabNavigationModel()
                          {
                              TabName = request.TabName,
                              LeadName = a.DisplayName,
                              LeadId = b.LeadID!,
                              ForAwardNoticeCount = a.OfferingList!.Where(l => l.SignedOfferSheetDate != null).Count(),
                              ForContractCount = a.OfferingList!.Where(l => !string.IsNullOrEmpty(l.TenantContractNo)).Count(),
                          }).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);

        }
    }
}
