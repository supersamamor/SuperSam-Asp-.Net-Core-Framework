using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;

public record EnrollPayeeCommand(IList<string> EnrolledPayeeIdList) : IRequest<bool>;

public class EnrollPayeeCommandHandler : IRequestHandler<EnrollPayeeCommand, bool>
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public EnrollPayeeCommandHandler(ApplicationContext context,
                                    IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(EnrollPayeeCommand request, CancellationToken cancellationToken) =>
        await AddEnrolledPayee(request, cancellationToken);


    public async Task<bool> AddEnrolledPayee(EnrollPayeeCommand request, CancellationToken cancellationToken)
    {
        var date = DateTime.Now.Date;
        var payeeToEnrollList = await _context.EnrolledPayee
            .Where(l => request.EnrolledPayeeIdList.Contains(l.Id)).AsNoTracking().ToListAsync(cancellationToken);
        var batchCount = (await _context.EnrollmentBatch
                             .Where(l => l.Date == date)
                             .AsNoTracking().CountAsync(cancellationToken: cancellationToken)) + 1;
        var enrollmentBatchToAdd = new EnrollmentBatchState() { Date = date, Batch = batchCount };
        await _context.AddAsync(enrollmentBatchToAdd, cancellationToken);
        //Todo: Use Bulk Update
        foreach (var item in payeeToEnrollList)
        {
            item.TagAsActiveAndSetBatch(enrollmentBatchToAdd.Id);           
        }
        _context.UpdateRange(payeeToEnrollList);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
