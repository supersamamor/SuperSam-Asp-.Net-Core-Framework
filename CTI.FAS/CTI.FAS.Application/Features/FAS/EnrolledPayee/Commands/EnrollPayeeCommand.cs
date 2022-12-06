using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.Common.Utility.Validators;
using CTI.FAS.Core.FAS;
using CTI.FAS.CsvGenerator.Services;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;

public record EnrollPayeeCommand(IList<string> EnrolledPayeeIdList) : IRequest<string>;

public class EnrollPayeeCommandHandler : IRequestHandler<EnrollPayeeCommand, string>
{
    private readonly ApplicationContext _context;
    private readonly PayeeEnrollmentCsvService _payeeEnrollmentCsvService;
    private readonly IAuthenticatedUser _authenticatedUser;
    public EnrollPayeeCommandHandler(ApplicationContext context, PayeeEnrollmentCsvService payeeEnrollmentCsvService,
        IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _payeeEnrollmentCsvService = payeeEnrollmentCsvService;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<string> Handle(EnrollPayeeCommand request, CancellationToken cancellationToken) =>
        await AddEnrolledPayee(request, cancellationToken);


    public async Task<string> AddEnrolledPayee(EnrollPayeeCommand request, CancellationToken cancellationToken)
    {
        var date = DateTime.Now.Date;
        var payeeToEnrollList = await _context.EnrolledPayee
            .Where(l => request.EnrolledPayeeIdList.Contains(l.Id)).AsNoTracking().ToListAsync(cancellationToken);
        var csvDocument = _payeeEnrollmentCsvService.Export(payeeToEnrollList, _authenticatedUser.UserId!);
        var batchCount = (await _context.EnrollmentBatch
                             .Where(l => l.Date == date)
                             .AsNoTracking().CountAsync(cancellationToken: cancellationToken)) + 1;
        var enrollmentBatchToAdd = new EnrollmentBatchState()
        {
            Date = date,
            Batch = batchCount,
            FilePath = csvDocument.CompleteFilePath,
            Url = csvDocument.FileUrl,
            UserId = _authenticatedUser.UserId,
        };
        await _context.AddAsync(enrollmentBatchToAdd, cancellationToken);
        //Todo: Use Bulk Update
        foreach (var item in payeeToEnrollList)
        {
            item.TagAsActiveAndSetBatch(enrollmentBatchToAdd.Id);
        }
        _context.UpdateRange(payeeToEnrollList);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return csvDocument.FileUrl;
    }
}
