using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DPI.Core.DPI;
using CTI.DPI.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;

public record EditReportColumnHeaderCommand : ReportColumnHeaderState, IRequest<Validation<Error, ReportColumnHeaderState>>;

public class EditReportColumnHeaderCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnHeaderState, EditReportColumnHeaderCommand>, IRequestHandler<EditReportColumnHeaderCommand, Validation<Error, ReportColumnHeaderState>>
{
    public EditReportColumnHeaderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportColumnHeaderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportColumnHeaderState>> Handle(EditReportColumnHeaderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditReportColumnHeader(request, cancellationToken));


	public async Task<Validation<Error, ReportColumnHeaderState>> EditReportColumnHeader(EditReportColumnHeaderCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ReportColumnHeader.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateReportColumnDetailList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportColumnHeaderState>(entity);
	}
	
	private async Task UpdateReportColumnDetailList(ReportColumnHeaderState entity, EditReportColumnHeaderCommand request, CancellationToken cancellationToken)
	{
		IList<ReportColumnDetailState> reportColumnDetailListForDeletion = new List<ReportColumnDetailState>();
		var queryReportColumnDetailForDeletion = Context.ReportColumnDetail.Where(l => l.ReportColumnId == request.Id).AsNoTracking();
		if (entity.ReportColumnDetailList?.Count > 0)
		{
			queryReportColumnDetailForDeletion = queryReportColumnDetailForDeletion.Where(l => !(entity.ReportColumnDetailList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportColumnDetailListForDeletion = await queryReportColumnDetailForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportColumnDetail in reportColumnDetailListForDeletion!)
		{
			Context.Entry(reportColumnDetail).State = EntityState.Deleted;
		}
		if (entity.ReportColumnDetailList?.Count > 0)
		{
			foreach (var reportColumnDetail in entity.ReportColumnDetailList.Where(l => !reportColumnDetailListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportColumnDetailState>(x => x.Id == reportColumnDetail.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportColumnDetail).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportColumnDetail).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditReportColumnHeaderCommandValidator : AbstractValidator<EditReportColumnHeaderCommand>
{
    readonly ApplicationContext _context;

    public EditReportColumnHeaderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportColumnHeaderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnHeader with id {PropertyValue} does not exists");
        
    }
}
