using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.ProjectNamePlaceHolder.MainModulePlaceHolder.Queries
{
    public record GetMainModulePlaceHolderByIdQuery(string Id) : IRequest<Option<Core.ProjectNamePlaceHolder.MainModulePlaceHolder>>;

    public class GetMainModulePlaceHolderByIdQueryHandler : IRequestHandler<GetMainModulePlaceHolderByIdQuery, Option<Core.ProjectNamePlaceHolder.MainModulePlaceHolder>>
    {
        private readonly ApplicationContext _context;

        public GetMainModulePlaceHolderByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Option<Core.ProjectNamePlaceHolder.MainModulePlaceHolder>> Handle(GetMainModulePlaceHolderByIdQuery request, CancellationToken cancellationToken) =>
            await _context.MainModulePlaceHolder.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
    }
}
