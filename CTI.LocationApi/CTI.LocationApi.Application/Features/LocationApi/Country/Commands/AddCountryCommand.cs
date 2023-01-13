using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.LocationApi.Application.Features.LocationApi.Country.Commands;

public record AddCountryCommand : CountryState, IRequest<Validation<Error, CountryState>>;

public class AddCountryCommandHandler : BaseCommandHandler<ApplicationContext, CountryState, AddCountryCommand>, IRequestHandler<AddCountryCommand, Validation<Error, CountryState>>
{
	private readonly IdentityContext _identityContext;
    public AddCountryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddCountryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, CountryState>> Handle(AddCountryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddCountry(request, cancellationToken));


	public async Task<Validation<Error, CountryState>> AddCountry(AddCountryCommand request, CancellationToken cancellationToken)
	{
		CountryState entity = Mapper.Map<CountryState>(request);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, CountryState>(entity);
	}
	
	
	private async Task AddApprovers(string countryId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Country).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = countryId,
				ApprovalList = new List<ApprovalState>()
			};
			foreach (var approverItem in approverList)
			{
				if (approverItem.ApproverType == ApproverTypes.User)
				{
					var approval = new ApprovalState()
					{
						Sequence = approverItem.Sequence,
						ApproverUserId = approverItem.ApproverUserId!,
					};
					if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
					{
						approval.EmailSendingStatus = SendingStatus.Pending;
					}
					approvalRecord.ApprovalList.Add(approval);
				}
				else if (approverItem.ApproverType == ApproverTypes.Role)
				{
					var userListWithRole = await (from a in _identityContext.Users
													join b in _identityContext.UserRoles on a.Id equals b.UserId
													join c in _identityContext.Roles on b.RoleId equals c.Id
													where c.Id == approverItem.ApproverRoleId
													select a.Id).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
					foreach (var userId in userListWithRole)
					{
						var approval = new ApprovalState()
						{
							Sequence = approverItem.Sequence,
							ApproverUserId = userId,
						};
						if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
						{
							approval.EmailSendingStatus = SendingStatus.Pending;
						}
						approvalRecord.ApprovalList.Add(approval);
					}
				}
			}
			await Context.AddAsync(approvalRecord, cancellationToken);
		}
	}
}

public class AddCountryCommandValidator : AbstractValidator<AddCountryCommand>
{
    readonly ApplicationContext _context;

    public AddCountryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<CountryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Country with id {PropertyValue} already exists");
        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => await _context.NotExists<CountryState>(x => x.Name == name, cancellationToken: cancellation)).WithMessage("Country with name {PropertyValue} already exists");
	RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<CountryState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Country with code {PropertyValue} already exists");
	
    }
}
