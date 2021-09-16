using AutoMapper;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record EditMainModulePlaceHolderCommand(
        string Id,
        Template:[InsertNewCQRSParameterTextHere]) : BaseCommand(Id), IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class EditMainModulePlaceHolderCommandHandler : IRequestHandler<EditMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;

        public EditMainModulePlaceHolderCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Id == request.Id, cancellationToken).MatchAsync(
                async mainModulePlaceHolder => await UpdateMainModulePlaceHolder(request, mainModulePlaceHolder, cancellationToken),
                () => Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"MainModulePlaceHolder with id {request.Id} does not exist"));

        async Task<Validation<Error, EditMainModulePlaceHolderCommand>> ValidateRequest(EditMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var validations = new List<Validation<Error, EditMainModulePlaceHolderCommand>>()
            {           
               Template:[InsertNewUniqueValidationFromEditCommandTextHere]
            };
            var errors = validations.Map(v => v.Match(_ => None, errors => Some(errors))) 
                                    .Bind(e => e) 
                                    .Bind(e => e) 
                                    .ToSeq(); 
            return errors.Count == 0
                ? Success<Error, EditMainModulePlaceHolderCommand>(request)
                : Validation<Error, EditMainModulePlaceHolderCommand>.Fail(errors);
        }

        async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> UpdateMainModulePlaceHolder(EditMainModulePlaceHolderCommand request, Core.AreaPlaceHolder.MainModulePlaceHolder mainModulePlaceHolder, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(
                async request =>
                {
                    _mapper.Map(request, mainModulePlaceHolder);
                    _context.Update(mainModulePlaceHolder);
                    _ = await _context.SaveChangesAsync(cancellationToken);
                    return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(mainModulePlaceHolder);
                });   
				
		Template:[InsertNewUniqueValidationMethodFromCommandForEditTextHere]
    }
}
