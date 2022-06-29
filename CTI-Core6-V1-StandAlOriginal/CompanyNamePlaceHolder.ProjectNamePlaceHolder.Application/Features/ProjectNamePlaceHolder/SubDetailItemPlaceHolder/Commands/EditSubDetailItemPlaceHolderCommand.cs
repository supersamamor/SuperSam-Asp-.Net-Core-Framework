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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;

public record EditSubDetailItemPlaceHolderCommand : SubDetailItemPlaceHolderState, IRequest<Validation<Error, SubDetailItemPlaceHolderState>>;

public class EditSubDetailItemPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemPlaceHolderState, EditSubDetailItemPlaceHolderCommand>, IRequestHandler<EditSubDetailItemPlaceHolderCommand, Validation<Error, SubDetailItemPlaceHolderState>>
{
    public EditSubDetailItemPlaceHolderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSubDetailItemPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailItemPlaceHolderState>> Handle(EditSubDetailItemPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditSubDetailItemPlaceHolderCommandValidator : AbstractValidator<EditSubDetailItemPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public EditSubDetailItemPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailItemPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItemPlaceHolder with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<SubDetailItemPlaceHolderState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("SubDetailItemPlaceHolder with code {PropertyValue} already exists");
	
    }
}
