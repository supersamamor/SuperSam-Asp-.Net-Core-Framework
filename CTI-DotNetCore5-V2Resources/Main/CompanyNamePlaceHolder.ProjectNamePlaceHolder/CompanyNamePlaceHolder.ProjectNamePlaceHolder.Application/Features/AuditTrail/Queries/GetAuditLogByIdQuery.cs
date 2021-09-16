using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using LanguageExt;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Queries
{
    public record GetAuditLogByIdQuery(int Id) : IRequest<Option<Audit>>;

    public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, Option<Audit>>
    {
        readonly ApplicationContext _context;

        public GetAuditLogByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Option<Audit>> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken) => 
            await _context.GetSingle<Audit>(e => e.Id == request.Id, cancellationToken);
    }
}
