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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailList.Commands;

public record EditSubDetailListCommand : SubDetailListState, IRequest<Validation<Error, SubDetailListState>>;

public class EditSubDetailListCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListState, EditSubDetailListCommand>, IRequestHandler<EditSubDetailListCommand, Validation<Error, SubDetailListState>>
{
    public EditSubDetailListCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSubDetailListCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailListState>> Handle(EditSubDetailListCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSubDetailListCommandValidator : AbstractValidator<EditSubDetailListCommand>
{
    readonly ApplicationContext _context;

    public EditSubDetailListCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailList with id {PropertyValue} does not exists");
        
    }
}
