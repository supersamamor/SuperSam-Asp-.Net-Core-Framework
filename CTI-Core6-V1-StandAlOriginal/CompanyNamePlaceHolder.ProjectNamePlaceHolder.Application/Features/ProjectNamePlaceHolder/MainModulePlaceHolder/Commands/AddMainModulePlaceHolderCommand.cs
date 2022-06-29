using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands;

public record AddMainModulePlaceHolderCommand : MainModulePlaceHolderState, IRequest<Validation<Error, MainModulePlaceHolderState>>;

public class AddMainModulePlaceHolderCommandHandler : BaseCommandHandler<ApplicationContext, MainModulePlaceHolderState, AddMainModulePlaceHolderCommand>, IRequestHandler<AddMainModulePlaceHolderCommand, Validation<Error, MainModulePlaceHolderState>>
{
    public AddMainModulePlaceHolderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMainModulePlaceHolderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, MainModulePlaceHolderState>> Handle(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
        await _validator.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddMainModulePlaceHolder(request, cancellationToken));


    public async Task<Validation<Error, MainModulePlaceHolderState>> AddMainModulePlaceHolder(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
    {
        MainModulePlaceHolderState entity = _mapper.Map<MainModulePlaceHolderState>(request);
        UpdateSubDetailItemPlaceHolderList(entity);
        UpdateSubDetailListPlaceHolderList(entity);
        _ = await _context.AddAsync(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return Success<Error, MainModulePlaceHolderState>(entity);
    }

    private void UpdateSubDetailItemPlaceHolderList(MainModulePlaceHolderState entity)
    {
        if (entity.SubDetailItemPlaceHolderList?.Count > 0)
        {
            foreach (var subDetailItemPlaceHolder in entity.SubDetailItemPlaceHolderList!)
            {
                _context.Entry(subDetailItemPlaceHolder).State = EntityState.Added;
            }
        }
    }
    private void UpdateSubDetailListPlaceHolderList(MainModulePlaceHolderState entity)
    {
        if (entity.SubDetailListPlaceHolderList?.Count > 0)
        {
            foreach (var subDetailListPlaceHolder in entity.SubDetailListPlaceHolderList!)
            {
                _context.Entry(subDetailListPlaceHolder).State = EntityState.Added;
            }
        }
    }

}

public class AddMainModulePlaceHolderCommandValidator : AbstractValidator<AddMainModulePlaceHolderCommand>
{
    readonly ApplicationContext _context;

    public AddMainModulePlaceHolderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModulePlaceHolder with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<MainModulePlaceHolderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("MainModulePlaceHolder with code {PropertyValue} already exists");

    }
}
