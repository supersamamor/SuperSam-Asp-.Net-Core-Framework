using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.TenantSales.Application.Repositories;
using CTI.TenantSales.Core.Constants;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Commands;

public record EditFailedTenantPOSSalesCommand : TenantPOSSalesState, IRequest<Validation<Error, TenantPOSSalesState>>;

public class EditFailedTenantPOSSalesCommandHandler : BaseCommandHandler<ApplicationContext, TenantPOSSalesState, EditFailedTenantPOSSalesCommand>, IRequestHandler<EditFailedTenantPOSSalesCommand, Validation<Error, TenantPOSSalesState>>
{
    private readonly TenantPOSSalesRepository _tenantPOSSalesRepository;
    public EditFailedTenantPOSSalesCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<EditFailedTenantPOSSalesCommand> validator,
                                    TenantPOSSalesRepository tenantPOSSalesRepository) : base(context, mapper, validator)
    {      
        _tenantPOSSalesRepository = tenantPOSSalesRepository;
    }

    public async Task<Validation<Error, TenantPOSSalesState>> Handle(EditFailedTenantPOSSalesCommand request, CancellationToken cancellationToken) =>
            await Validators.ValidateTAsync(request, cancellationToken).BindT(
                async request => await EditTenantPOSSales(request, cancellationToken));

    private async Task<Validation<Error, TenantPOSSalesState>> EditTenantPOSSales(EditFailedTenantPOSSalesCommand request, CancellationToken cancellationToken = default)
    {
        return await Context.GetSingle<TenantPOSSalesState>(e => e.Id == request.Id, cancellationToken: cancellationToken).MatchAsync(
           Some: async entity =>
           {
               Mapper.Map(request, entity);               
               await _tenantPOSSalesRepository.UpdateTenantPOSSales(entity);
               _ = await Context.SaveChangesAsync(cancellationToken);
               return Success<Error, TenantPOSSalesState>(entity);
           },
           None: () => Fail<Error, TenantPOSSalesState>($"Record with id {request.Id} does not exist"));
    }

    public class EditFailedTenantPOSSalesCommandValidator : AbstractValidator<EditFailedTenantPOSSalesCommand>
    {
        readonly ApplicationContext _context;

        public EditFailedTenantPOSSalesCommandValidator(ApplicationContext context)
        {
            _context = context;
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TenantPOSSalesState>(x => x.Id == id, cancellationToken: cancellation))
                              .WithMessage("TenantPOSSales with id {PropertyValue} does not exists");
            RuleFor(x => x.ValidationStatus).NotEqual(Convert.ToInt32(ValidationStatusEnum.Passed)).WithMessage("Sales is already passed!");
        }
    }
}