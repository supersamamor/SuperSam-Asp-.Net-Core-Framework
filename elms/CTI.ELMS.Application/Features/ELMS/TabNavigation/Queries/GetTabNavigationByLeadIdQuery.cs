﻿using CTI.ELMS.Application.Features.ELMS.TabNavigation.Models;
using CTI.ELMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries
{
    public record GetTabNavigationByLeadIdQuery(string LeadId, string TabName) : IRequest<TabNavigationModel>;
    public class GetTabNavigationByLeadIdQueryHandler : IRequestHandler<GetTabNavigationByLeadIdQuery, TabNavigationModel>
    {
        private readonly ApplicationContext _context;
        public GetTabNavigationByLeadIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TabNavigationModel> Handle(GetTabNavigationByLeadIdQuery request, CancellationToken cancellationToken)
        {
            return await (from a in _context.Lead.Include(l => l.OfferingList)
                          where a.Id == request.LeadId
                          select new TabNavigationModel()
                          {
                              TabName = request.TabName,
                              LeadName = a.DisplayName,
                              LeadId = request.LeadId,
                              ForAwardNoticeCount = a.OfferingList!.Where(l => l.SignedOfferSheetDate != null).Count(),
                              ForContractCount = a.OfferingList!.Where(l => !string.IsNullOrEmpty(l.TenantContractNo)).Count(),
                          }).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);

        }
    }
}
