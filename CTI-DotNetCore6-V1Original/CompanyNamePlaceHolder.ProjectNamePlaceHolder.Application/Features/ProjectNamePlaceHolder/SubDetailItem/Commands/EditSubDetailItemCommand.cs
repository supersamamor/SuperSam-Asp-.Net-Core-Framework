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

public record EditSubDetailItemCommand : SubDetailItemState, IRequest<Validation<Error, SubDetailItemState>>;

public class EditSubDetailItemCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemState, EditSubDetailItemCommand>, IRequestHandler<EditSubDetailItemCommand, Validation<Error, SubDetailItemState>>
{
    public EditSubDetailItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSubDetailItemCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailItemState>> Handle(EditSubDetailItemCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSubDetailItemCommandValidator : AbstractValidator<EditSubDetailItemCommand>
{
    readonly ApplicationContext _context;

    public EditSubDetailItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItem with id {PropertyValue} does not exists");
        
    }
}
