using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands
{
    public record DeleteMainModulePlaceHolderCommand(string Id) : BaseCommand(Id), IRequest<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>;

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public DeleteProjectCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Validation<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.GetSingle((Core.AreaPlaceHolder.MainModulePlaceHolder p) => p.Id == request.Id, cancellationToken);
            return await project.MatchAsync(
                Some: async p =>
                 {
                     _context.MainModulePlaceHolder.Remove(p);
                     await _context.SaveChangesAsync(cancellationToken);
                     return Success<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(p);
                 },
                None: () =>
                {
                    return Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>($"Project not found");
                });
        }
    }
}
