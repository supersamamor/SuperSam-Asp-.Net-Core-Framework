using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AuditTrail.Commands
{
    public class AddAuditLogCommand : Audit, IRequest<Validation<Error, Audit>>
    {
    }

    public class AddAuditLogCommandHandler : IRequestHandler<AddAuditLogCommand, Validation<Error, Audit>>
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;

        public AddAuditLogCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Audit>> Handle(AddAuditLogCommand request, CancellationToken cancellationToken)
        {
            var log = _mapper.Map<Audit>(request);
            log.DateTime = log.DateTime == DateTime.MinValue ? DateTime.UtcNow : log.DateTime;
            _context.Add(log);
            _ = await _context.SaveChangesAsync(cancellationToken);
            return Success<Error, Audit>(log);
        }
    }
}
