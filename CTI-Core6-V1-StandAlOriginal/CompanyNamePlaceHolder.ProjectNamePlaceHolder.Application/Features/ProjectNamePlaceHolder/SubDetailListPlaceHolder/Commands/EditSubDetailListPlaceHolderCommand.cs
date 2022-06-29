using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;

public record EditSubDetailListPlaceHolderCommand : SubDetailListPlaceHolderState, IRequest<Validation<Error, SubDetailListPlaceHolderState>>;

public class EditSubDetailListPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListPlaceHolderState, EditSubDetailListPlaceHolderCommand>, IRequestHandler<EditSubDetailListPlaceHolderCommand, Validation<Error, SubDetailListPlaceHolderState>>
{
    public EditSubDetailListPlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSubDetailListPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailListPlaceHolderState>> Handle(EditSubDetailListPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSubDetailListPlaceHolderCommandValidator : AbstractValidator<EditSubDetailListPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditSubDetailListPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailListPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailListPlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<SubDetailListPlaceHolderState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("SubDetailListPlaceHolder with code {PropertyValue} already exists");
	
    }
}
