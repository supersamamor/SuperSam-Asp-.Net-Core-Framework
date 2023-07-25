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

namespace CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Commands;

public record EditReportFilterGroupingCommand : ReportFilterGroupingState, IRequest<Validation<Error, ReportFilterGroupingState>>;

public class EditReportFilterGroupingCommandHandler : BaseCommandHandler<ApplicationContext, ReportFilterGroupingState, EditReportFilterGroupingCommand>, IRequestHandler<EditReportFilterGroupingCommand, Validation<Error, ReportFilterGroupingState>>
{
    public EditReportFilterGroupingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportFilterGroupingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportFilterGroupingState>> Handle(EditReportFilterGroupingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditReportFilterGrouping(request, cancellationToken));


	public async Task<Validation<Error, ReportFilterGroupingState>> EditReportFilterGrouping(EditReportFilterGroupingCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ReportFilterGrouping.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateReportColumnFilterList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportFilterGroupingState>(entity);
	}
	
	private async Task UpdateReportColumnFilterList(ReportFilterGroupingState entity, EditReportFilterGroupingCommand request, CancellationToken cancellationToken)
	{
		IList<ReportColumnFilterState> reportColumnFilterListForDeletion = new List<ReportColumnFilterState>();
		var queryReportColumnFilterForDeletion = Context.ReportColumnFilter.Where(l => l.ReportFilterGroupingId == request.Id).AsNoTracking();
		if (entity.ReportColumnFilterList?.Count > 0)
		{
			queryReportColumnFilterForDeletion = queryReportColumnFilterForDeletion.Where(l => !(entity.ReportColumnFilterList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportColumnFilterListForDeletion = await queryReportColumnFilterForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportColumnFilter in reportColumnFilterListForDeletion!)
		{
			Context.Entry(reportColumnFilter).State = EntityState.Deleted;
		}
		if (entity.ReportColumnFilterList?.Count > 0)
		{
			foreach (var reportColumnFilter in entity.ReportColumnFilterList.Where(l => !reportColumnFilterListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportColumnFilterState>(x => x.Id == reportColumnFilter.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportColumnFilter).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportColumnFilter).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditReportFilterGroupingCommandValidator : AbstractValidator<EditReportFilterGroupingCommand>
{
    readonly ApplicationContext _context;

    public EditReportFilterGroupingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportFilterGroupingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportFilterGrouping with id {PropertyValue} does not exists");
        
    }
}
