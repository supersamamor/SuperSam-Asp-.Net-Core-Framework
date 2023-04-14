using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Commands;

public record DeleteCustomerCommand : BaseCommand, IRequest<Validation<Error, CustomerState>>;

public class DeleteCustomerCommandHandler : BaseCommandHandler<ApplicationContext, CustomerState, DeleteCustomerCommand>, IRequestHandler<DeleteCustomerCommand, Validation<Error, CustomerState>>
{
    public DeleteCustomerCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCustomerCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CustomerState>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    readonly ApplicationContext _context;

    public DeleteCustomerCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomerState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Customer with id {PropertyValue} does not exists");
    }
}
