using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersByPrimaryKey
{
    public class GetMainModulePlaceHolderByPrimaryKeyQuery : IRequest<MainModulePlaceHolderDto>
    {
        public string PrimaryKey { get; set; } = "";
    }

    public class GetMainModulePlaceHolderByProjectQueryHandler : IRequestHandler<GetMainModulePlaceHolderByPrimaryKeyQuery, MainModulePlaceHolderDto>
    {
        private readonly IRepository<Domain.Entities.MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHolderByProjectQueryHandler(IRepository<Domain.Entities.MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<MainModulePlaceHolderDto> Handle(GetMainModulePlaceHolderByPrimaryKeyQuery request, CancellationToken cancellationToken)
        {
            var spec = new MainModulePlaceHolderByPrimaryKeyWithDetailsSpec(request.PrimaryKey);
            var unitsDto = await _mainModulePlaceHolderRepository.SingleOrDefaultAsync(spec);
            return unitsDto;
        }
    }

    public class MainModulePlaceHolderByPrimaryKeyWithDetailsSpec : SingleResultSpecification<Domain.Entities.MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public MainModulePlaceHolderByPrimaryKeyWithDetailsSpec(string primaryKey)
        {
            Query.Where(x => x.PrimaryKey == primaryKey);
        }
    }
}
