using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions;
using LanguageExt;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Commands
{
    public record DeleteMainModulePlaceHolderCommand(string Id) : BaseCommand(Id), IRequest<Validation<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>>;

    public class DeleteMainModulePlaceHolderCommandHandler : IRequestHandler<DeleteMainModulePlaceHolderCommand, Validation<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public DeleteMainModulePlaceHolderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Validation<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>> Handle(DeleteMainModulePlaceHolderCommand request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolder = await _context.GetSingle((Core.ProjectNamePlaceHolder.MainModulePlaceHolder p) => p.Id == request.Id, cancellationToken);
            return await mainModulePlaceHolder.MatchAsync(
                Some: async p =>
                 {
                     _context.MainModulePlaceHolder.Remove(p);
                     await _context.SaveChangesAsync(cancellationToken);
                     return Success<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>(p);
                 },
                None: () =>
                {
                    return Fail<Error, Core.ProjectNamePlaceHolder.MainModulePlaceHolder>($"MainModulePlaceHolder not found");
                });
        }
    }
}
