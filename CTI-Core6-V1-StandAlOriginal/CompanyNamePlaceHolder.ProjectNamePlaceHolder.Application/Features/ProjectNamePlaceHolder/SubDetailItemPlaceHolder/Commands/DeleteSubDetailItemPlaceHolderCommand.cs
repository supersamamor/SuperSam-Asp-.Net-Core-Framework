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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailItemPlaceHolder.Commands;

public record DeleteSubDetailItemPlaceHolderCommand : BaseCommand, IRequest<Validation<Error, SubDetailItemPlaceHolderState>>;

public class DeleteSubDetailItemPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailItemPlaceHolderState, DeleteSubDetailItemPlaceHolderCommand>, IRequestHandler<DeleteSubDetailItemPlaceHolderCommand, Validation<Error, SubDetailItemPlaceHolderState>>
{
    public DeleteSubDetailItemPlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSubDetailItemPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SubDetailItemPlaceHolderState>> Handle(DeleteSubDetailItemPlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSubDetailItemPlaceHolderCommandValidator : AbstractValidator<DeleteSubDetailItemPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteSubDetailItemPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailItemPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailItemPlaceHolder with id {PropertyValue} does not exists");
    }
}
