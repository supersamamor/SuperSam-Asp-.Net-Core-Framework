using AutoMapper;
using LanguageExt;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record EditMainModulePlaceHolderCommand : Core.AreaPlaceHolder.MainModulePlaceHolder, IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class EditMainModulePlaceHolderCommandHandler : BaseAreaPlaceHolderCommandHandler<EditMainModulePlaceHolderCommand>, IRequestHandler<EditMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        public EditMainModulePlaceHolderCommandHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Id == request.Id, cancellationToken, true).MatchAsync(
                async entity => await UpdateMainModulePlaceHolder(request, entity, cancellationToken),
                () => Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"MainModulePlaceHolder with id {request.Id} does not exist"));

        async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> UpdateMainModulePlaceHolder(EditMainModulePlaceHolderCommand request, Core.AreaPlaceHolder.MainModulePlaceHolder entity, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(
                async request => await Edit(request, entity, cancellationToken));

        async Task<Validation<Error, EditMainModulePlaceHolderCommand>> ValidateRequest(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var validations = List(           
                Template:[InsertNewUniqueValidationFromEditCommandTextHere]
			);
            var errors = validations.Bind(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Seq<Error>>
                                    .Bind(e => e) //IEnumerable<Error>
                                    .ToSeq(); // Seq<Error>
            return errors.Count == 0
                ? Success<Error, EditMainModulePlaceHolderCommand>(request)
                : Validation<Error, EditMainModulePlaceHolderCommand>.Fail(errors);
        }
 
        Template:[InsertNewUniqueValidationMethodFromCommandForEditTextHere]
    }
}
