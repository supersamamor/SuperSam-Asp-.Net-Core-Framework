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

public record AddSubDetailListCommand : SubDetailListState, IRequest<Validation<Error, SubDetailListState>>;

public class AddSubDetailListCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListState, AddSubDetailListCommand>, IRequestHandler<AddSubDetailListCommand, Validation<Error, SubDetailListState>>
{
    public AddSubDetailListCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSubDetailListCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, SubDetailListState>> Handle(AddSubDetailListCommand request, CancellationToken cancellationToken) =>
		await _validator.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddSubDetailListCommandValidator : AbstractValidator<AddSubDetailListCommand>
{
    readonly ApplicationContext _context;

    public AddSubDetailListCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SubDetailListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailList with id {PropertyValue} already exists");
        
    }
}
