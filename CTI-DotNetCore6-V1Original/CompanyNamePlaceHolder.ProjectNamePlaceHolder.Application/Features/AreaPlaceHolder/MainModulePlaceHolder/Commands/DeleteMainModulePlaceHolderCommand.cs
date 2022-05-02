using AutoMapper;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
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
    public record DeleteMainModulePlaceHolderCommand : BaseCommand, IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class DeleteMainModulePlaceHolderCommandHandler : IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public DeleteMainModulePlaceHolderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GetSingle<Core.AreaPlaceHolder.MainModulePlaceHolder>(p => p.Id == request.Id, cancellationToken, true);
            return await entity.MatchAsync(
                Some: async p =>
                 {
                     _context.MainModulePlaceHolder.Remove(p);
                     await _context.SaveChangesAsync(cancellationToken);
                     return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(p);
                 },
                None: () =>
                {
                    return Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"MainModulePlaceHolder not found");
                });
        }
    }
}
