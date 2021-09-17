using AutoMapper;
using LanguageExt;
using MediatR;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record AddMainModulePlaceHolderCommand : Core.AreaPlaceHolder.MainModulePlaceHolder, IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class AddMainModulePlaceHolderCommandHandler : BaseAreaPlaceHolderCommandHandler<AddMainModulePlaceHolderCommand>, IRequestHandler<AddMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        public AddMainModulePlaceHolderCommandHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await ValidateRequest(request, cancellationToken).BindT(
                async request => await Add<Core.AreaPlaceHolder.MainModulePlaceHolder>(request, cancellationToken));

        async Task<Validation<Error, AddMainModulePlaceHolderCommand>> ValidateRequest(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var validations = List(
                await ValidateId(request, cancellationToken)            
                ,Template:[InsertNewUniqueValidationFromCommandTextHere]
			);
            var errors = validations.Bind(v => v.Match(_ => None, errors => Some(errors))) //IEnumerable<Seq<Error>>
                                    .Bind(e => e) //IEnumerable<Error>
                                    .ToSeq(); // Seq<Error>
            return errors.Count == 0
                ? Success<Error, AddMainModulePlaceHolderCommand>(request)
                : Validation<Error, AddMainModulePlaceHolderCommand>.Fail(errors);
        }

        async Task<Validation<Error, AddMainModulePlaceHolderCommand>> ValidateId(AddMainModulePlaceHolderCommand request, CancellationToken cancellationToken) =>
            await _context.MustNotExist<Core.AreaPlaceHolder.MainModulePlaceHolder, AddMainModulePlaceHolderCommand>(p => p.Id == request.Id,
                                                                         request,
                                                                         $"MainModulePlaceHolder with id {request.Id} already exists",
                                                                         cancellationToken);
																		 
		Template:[InsertNewUniqueValidationMethodFromCommandTextHere]
    }
}
