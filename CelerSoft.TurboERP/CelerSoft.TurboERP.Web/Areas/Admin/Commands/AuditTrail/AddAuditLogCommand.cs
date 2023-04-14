using AutoMapper;
using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.Common.Data;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Commands.AuditTrail;

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
