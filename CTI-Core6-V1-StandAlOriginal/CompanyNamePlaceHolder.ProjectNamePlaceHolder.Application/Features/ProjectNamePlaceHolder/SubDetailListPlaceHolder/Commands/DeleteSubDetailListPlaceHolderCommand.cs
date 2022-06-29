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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.SubDetailListPlaceHolder.Commands;

public record DeleteSubDetailListPlaceHolderCommand : BaseCommand, IRequest<Validation<Error, SubDetailListPlaceHolderState>>;

public class DeleteSubDetailListPlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, SubDetailListPlaceHolderState, DeleteSubDetailListPlaceHolderCommand>, IRequestHandler<DeleteSubDetailListPlaceHolderCommand, Validation<Error, SubDetailListPlaceHolderState>>
{
    public DeleteSubDetailListPlaceHolderCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteSubDetailListPlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SubDetailListPlaceHolderState>> Handle(DeleteSubDetailListPlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteSubDetailListPlaceHolderCommandValidator : AbstractValidator<DeleteSubDetailListPlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public DeleteSubDetailListPlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SubDetailListPlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SubDetailListPlaceHolder with id {PropertyValue} does not exists");
    }
}
