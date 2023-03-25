using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersByTemplate[PK]
{
    public class GetMainModulePlaceHolderByTemplate[PK]Query : IRequest<MainModulePlaceHolderDto>
    {
        public string Template[PK] { get; set; } = "";
    }

    public class GetMainModulePlaceHolderByProjectQueryHandler : IRequestHandler<GetMainModulePlaceHolderByTemplate[PK]Query, MainModulePlaceHolderDto>
    {
        private readonly IRepository<Domain.Entities.MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHolderByProjectQueryHandler(IRepository<Domain.Entities.MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<MainModulePlaceHolderDto> Handle(GetMainModulePlaceHolderByTemplate[PK]Query request, CancellationToken cancellationToken)
        {
            var spec = new MainModulePlaceHolderByTemplate[PK]WithDetailsSpec(request.Template[PK]);
            var unitsDto = await _mainModulePlaceHolderRepository.SingleOrDefaultAsync(spec);
            return unitsDto;
        }
    }

    public class MainModulePlaceHolderByTemplate[PK]WithDetailsSpec : SingleResultSpecification<Domain.Entities.MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public MainModulePlaceHolderByTemplate[PK]WithDetailsSpec(string Template[InsertNewPrimaryKeyCamelCaseTextHere])
        {
            Query.Where(x => x.Template[PK] == Template[InsertNewPrimaryKeyCamelCaseTextHere]);
        }
    }
}
