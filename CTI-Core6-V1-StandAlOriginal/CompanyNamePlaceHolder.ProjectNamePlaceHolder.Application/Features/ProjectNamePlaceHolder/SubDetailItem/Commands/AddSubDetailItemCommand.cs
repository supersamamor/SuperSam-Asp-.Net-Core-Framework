using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItem.Commands;

public record AddSubDetailItemCommand : SubDetailItemState, IRequest<Validation<Error, SubDetailItemState>>;

public class AddSubDetailItemCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemState, AddSubDetailItemCommand>, IRequestHandler<AddSubDetailItemCommand, Validation<Error, SubDetailItemState>>
{
    public AddSubDetailItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSubDetailItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SubDetailItemState>> Handle(AddSubDetailItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSubDetailItem(request, cancellationToken));


	public async Task<Validation<Error, SubDetailItemState>> AddSubDetailItem(AddSubDetailItemCommand request, CancellationToken cancellationToken)
	{
		SubDetailItemState entity = Mapper.Map<SubDetailItemState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SubDetailItemState>(entity);
	}
	
	
	
}

public class AddSubDetailItemCommandValidator : AbstractValidator<AddSubDetailItemCommand>
{
    readonly ApplicationContext _context;

    public AddSubDetailItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SubDetailItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItem with id {PropertyValue} already exists");
        
    }
}
