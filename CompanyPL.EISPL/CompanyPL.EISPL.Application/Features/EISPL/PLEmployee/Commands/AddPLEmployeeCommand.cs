using AutoMapper;
using CompanyPL.Common.Core.Commands;
using CompanyPL.Common.Data;
using CompanyPL.Common.Utility.Validators;
using CompanyPL.EISPL.Core.EISPL;
using CompanyPL.EISPL.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyPL.EISPL.Application.Features.EISPL.PLEmployee.Commands;

public record AddPLEmployeeCommand : PLEmployeeState, IRequest<Validation<Error, PLEmployeeState>>;

public class AddPLEmployeeCommandHandler : BaseCommandHandler<ApplicationContext, PLEmployeeState, AddPLEmployeeCommand>, IRequestHandler<AddPLEmployeeCommand, Validation<Error, PLEmployeeState>>
{
	private readonly IdentityContext _identityContext;
    public AddPLEmployeeCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPLEmployeeCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, PLEmployeeState>> Handle(AddPLEmployeeCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddPLEmployee(request, cancellationToken));


	public async Task<Validation<Error, PLEmployeeState>> AddPLEmployee(AddPLEmployeeCommand request, CancellationToken cancellationToken)
	{
		PLEmployeeState entity = Mapper.Map<PLEmployeeState>(request);
		UpdatePLContactInformationList(entity);
		UpdatePLHealthDeclarationList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, PLEmployeeState>(entity);
	}
	
	private void UpdatePLContactInformationList(PLEmployeeState entity)
	{
		if (entity.PLContactInformationList?.Count > 0)
		{
			foreach (var pLContactInformation in entity.PLContactInformationList!)
			{
				Context.Entry(pLContactInformation).State = EntityState.Added;
			}
		}
	}
	private void UpdatePLHealthDeclarationList(PLEmployeeState entity)
	{
		if (entity.PLHealthDeclarationList?.Count > 0)
		{
			foreach (var pLHealthDeclaration in entity.PLHealthDeclarationList!)
			{
				Context.Entry(pLHealthDeclaration).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddPLEmployeeCommandValidator : AbstractValidator<AddPLEmployeeCommand>
{
    readonly ApplicationContext _context;

    public AddPLEmployeeCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PLEmployeeState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PLEmployee with id {PropertyValue} already exists");
        RuleFor(x => x.PLEmployeeCode).MustAsync(async (pLEmployeeCode, cancellation) => await _context.NotExists<PLEmployeeState>(x => x.PLEmployeeCode == pLEmployeeCode, cancellationToken: cancellation)).WithMessage("PLEmployee with pLEmployeeCode {PropertyValue} already exists");
	
    }
}
