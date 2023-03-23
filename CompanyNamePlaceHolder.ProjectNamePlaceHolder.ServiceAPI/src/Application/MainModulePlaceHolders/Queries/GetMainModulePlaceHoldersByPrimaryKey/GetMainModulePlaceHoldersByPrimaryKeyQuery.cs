using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.DTOs;
using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByPrimaryKey
{
    public class GetMainModulePlaceHoldersByPrimaryKeyQuery : IRequest<MainModulePlaceHolderDto>
    {
        public string PrimaryKey { get; set; } = "";
    }

    public class GetMainModulePlaceHoldersByProjectQueryHandler : IRequestHandler<GetMainModulePlaceHoldersByPrimaryKeyQuery, MainModulePlaceHolderDto>
    {
        private readonly IRepository<MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHoldersByProjectQueryHandler(IRepository<MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<MainModulePlaceHolderDto> Handle(GetMainModulePlaceHoldersByPrimaryKeyQuery request, CancellationToken cancellationToken)
        {
            var spec = new MainModulePlaceHolderByPrimaryKeyWithDetailsSpec(request.PrimaryKey);
            var unitsDto = await _mainModulePlaceHolderRepository.SingleOrDefaultAsync(spec);
            return unitsDto;
        }
    }

    public class MainModulePlaceHolderByPrimaryKeyWithDetailsSpec : SingleResultSpecification<MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public MainModulePlaceHolderByPrimaryKeyWithDetailsSpec(string primaryKey)
        {
            Query.Where(x => x.PrimaryKey == primaryKey);
        }
    }
}
