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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;

public record AddSubDetailListPlaceHolderCommand : SubDetailListPlaceHolderState, IRequest<Validation<Error, SubDetailListPlaceHolderState>>;

public class AddSubDetailListPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListPlaceHolderState, AddSubDetailListPlaceHolderCommand>, IRequestHandler<AddSubDetailListPlaceHolderCommand, Validation<Error, SubDetailListPlaceHolderState>>
{
    public AddSubDetailListPlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSubDetailListPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailListPlaceHolderState>> Handle(AddSubDetailListPlaceHolderCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddSubDetailListPlaceHolderCommandValidator : AbstractValidator<AddSubDetailListPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddSubDetailListPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SubDetailListPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailListPlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<SubDetailListPlaceHolderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("SubDetailListPlaceHolder with code {PropertyValue} already exists");
	
    }
}
