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

namespace CTI.DPI.Application.Features.DPI.ReportTable.Commands;

public record EditReportTableCommand : ReportTableState, IRequest<Validation<Error, ReportTableState>>;

public class EditReportTableCommandHandler : BaseCommandHandler<ApplicationContext, ReportTableState, EditReportTableCommand>, IRequestHandler<EditReportTableCommand, Validation<Error, ReportTableState>>
{
    public EditReportTableCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportTableCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportTableState>> Handle(EditReportTableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditReportTable(request, cancellationToken));


	public async Task<Validation<Error, ReportTableState>> EditReportTable(EditReportTableCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ReportTable.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateReportTableJoinParameterList(entity, request, cancellationToken);
		await UpdateReportColumnDetailList(entity, request, cancellationToken);
		await UpdateReportColumnFilterList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportTableState>(entity);
	}
	
	private async Task UpdateReportTableJoinParameterList(ReportTableState entity, EditReportTableCommand request, CancellationToken cancellationToken)
	{
		IList<ReportTableJoinParameterState> reportTableJoinParameterListForDeletion = new List<ReportTableJoinParameterState>();
		var queryReportTableJoinParameterForDeletion = Context.ReportTableJoinParameter.Where(l => l.TableId == request.Id).AsNoTracking();
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			queryReportTableJoinParameterForDeletion = queryReportTableJoinParameterForDeletion.Where(l => !(entity.ReportTableJoinParameterList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		reportTableJoinParameterListForDeletion = await queryReportTableJoinParameterForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var reportTableJoinParameter in reportTableJoinParameterListForDeletion!)
		{
			Context.Entry(reportTableJoinParameter).State = EntityState.Deleted;
		}
		if (entity.ReportTableJoinParameterList?.Count > 0)
		{
			foreach (var reportTableJoinParameter in entity.ReportTableJoinParameterList.Where(l => !reportTableJoinParameterListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ReportTableJoinParameterState>(x => x.Id == reportTableJoinParameter.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(reportTableJoinParameter).State = EntityState.Added;
				}
				else
				{
					Context.Entry(reportTableJoinParameter).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportColumnDetailList(ReportTableState entity, EditReportTableCommand request, CancellationToken cancellationToken)
	{
		IList<ReportColumnDetailState> reportColumnDetailListForDeletion = new List<ReportColumnDetailState>();
		var queryReportColumnDetailForDeletion = Context.ReportColumnDetail.Where(l => l.TableId == request.Id).AsNoTracking();
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
	private async Task UpdateReportColumnFilterList(ReportTableState entity, EditReportTableCommand request, CancellationToken cancellationToken)
	{
		IList<ReportColumnFilterState> reportColumnFilterListForDeletion = new List<ReportColumnFilterState>();
		var queryReportColumnFilterForDeletion = Context.ReportColumnFilter.Where(l => l.TableId == request.Id).AsNoTracking();
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

public class EditReportTableCommandValidator : AbstractValidator<EditReportTableCommand>
{
    readonly ApplicationContext _context;

    public EditReportTableCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportTableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportTable with id {PropertyValue} does not exists");
        
    }
}
