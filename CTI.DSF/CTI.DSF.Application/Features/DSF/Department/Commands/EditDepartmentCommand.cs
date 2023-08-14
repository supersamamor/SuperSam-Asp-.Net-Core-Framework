using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.Department.Commands;

public record EditDepartmentCommand : DepartmentState, IRequest<Validation<Error, DepartmentState>>;

public class EditDepartmentCommandHandler : BaseCommandHandler<ApplicationContext, DepartmentState, EditDepartmentCommand>, IRequestHandler<EditDepartmentCommand, Validation<Error, DepartmentState>>
{
    public EditDepartmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDepartmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DepartmentState>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditDepartment(request, cancellationToken));


	public async Task<Validation<Error, DepartmentState>> EditDepartment(EditDepartmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Department.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateSectionList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DepartmentState>(entity);
	}
	
	private async Task UpdateSectionList(DepartmentState entity, EditDepartmentCommand request, CancellationToken cancellationToken)
	{
		IList<SectionState> sectionListForDeletion = new List<SectionState>();
		var querySectionForDeletion = Context.Section.Where(l => l.DepartmentCode == request.Id).AsNoTracking();
		if (entity.SectionList?.Count > 0)
		{
			querySectionForDeletion = querySectionForDeletion.Where(l => !(entity.SectionList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		sectionListForDeletion = await querySectionForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var section in sectionListForDeletion!)
		{
			Context.Entry(section).State = EntityState.Deleted;
		}
		if (entity.SectionList?.Count > 0)
		{
			foreach (var section in entity.SectionList.Where(l => !sectionListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<SectionState>(x => x.Id == section.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(section).State = EntityState.Added;
				}
				else
				{
					Context.Entry(section).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditDepartmentCommandValidator : AbstractValidator<EditDepartmentCommand>
{
    readonly ApplicationContext _context;

    public EditDepartmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DepartmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Department with id {PropertyValue} does not exists");
        RuleFor(x => x.DepartmentCode).MustAsync(async (request, departmentCode, cancellation) => await _context.NotExists<DepartmentState>(x => x.DepartmentCode == departmentCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Department with departmentCode {PropertyValue} already exists");
	RuleFor(x => x.DepartmentName).MustAsync(async (request, departmentName, cancellation) => await _context.NotExists<DepartmentState>(x => x.DepartmentName == departmentName && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Department with departmentName {PropertyValue} already exists");
	
    }
}
