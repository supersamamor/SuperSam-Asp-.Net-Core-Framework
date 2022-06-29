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

public record AddSubDetailItemPlaceHolderCommand : SubDetailItemPlaceHolderState, IRequest<Validation<Error, SubDetailItemPlaceHolderState>>;

public class AddSubDetailItemPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemPlaceHolderState, AddSubDetailItemPlaceHolderCommand>, IRequestHandler<AddSubDetailItemPlaceHolderCommand, Validation<Error, SubDetailItemPlaceHolderState>>
{
    public AddSubDetailItemPlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSubDetailItemPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailItemPlaceHolderState>> Handle(AddSubDetailItemPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddSubDetailItemPlaceHolderCommandValidator : AbstractValidator<AddSubDetailItemPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddSubDetailItemPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SubDetailItemPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItemPlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<SubDetailItemPlaceHolderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("SubDetailItemPlaceHolder with code {PropertyValue} already exists");
	
    }
}
