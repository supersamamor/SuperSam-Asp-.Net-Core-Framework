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

public record AddReportColumnHeaderCommand : ReportColumnHeaderState, IRequest<Validation<Error, ReportColumnHeaderState>>;

public class AddReportColumnHeaderCommandHandler : BaseCommandHandler<ApplicationContext, ReportColumnHeaderState, AddReportColumnHeaderCommand>, IRequestHandler<AddReportColumnHeaderCommand, Validation<Error, ReportColumnHeaderState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportColumnHeaderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportColumnHeaderCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportColumnHeaderState>> Handle(AddReportColumnHeaderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReportColumnHeader(request, cancellationToken));


	public async Task<Validation<Error, ReportColumnHeaderState>> AddReportColumnHeader(AddReportColumnHeaderCommand request, CancellationToken cancellationToken)
	{
		ReportColumnHeaderState entity = Mapper.Map<ReportColumnHeaderState>(request);
		UpdateReportColumnDetailList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportColumnHeaderState>(entity);
	}
	
	private void UpdateReportColumnDetailList(ReportColumnHeaderState entity)
	{
		if (entity.ReportColumnDetailList?.Count > 0)
		{
			foreach (var reportColumnDetail in entity.ReportColumnDetailList!)
			{
				Context.Entry(reportColumnDetail).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddReportColumnHeaderCommandValidator : AbstractValidator<AddReportColumnHeaderCommand>
{
    readonly ApplicationContext _context;

    public AddReportColumnHeaderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportColumnHeaderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportColumnHeader with id {PropertyValue} already exists");
        
    }
}
