using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersByTemplate:[InsertNewPrimaryKeyTextHere]
{
    public class GetMainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]Query : IRequest<MainModulePlaceHolderDto>
    {
        public string Template:[InsertNewPrimaryKeyTextHere] { get; set; } = "";
    }

    public class GetMainModulePlaceHolderByProjectQueryHandler : IRequestHandler<GetMainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]Query, MainModulePlaceHolderDto>
    {
        private readonly IRepository<Domain.Entities.MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHolderByProjectQueryHandler(IRepository<Domain.Entities.MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<MainModulePlaceHolderDto> Handle(GetMainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]Query request, CancellationToken cancellationToken)
        {
            var spec = new MainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]WithDetailsSpec(request.Template:[InsertNewPrimaryKeyTextHere]);
            var unitsDto = await _mainModulePlaceHolderRepository.SingleOrDefaultAsync(spec);
            return unitsDto;
        }
    }

    public class MainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]WithDetailsSpec : SingleResultSpecification<Domain.Entities.MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public MainModulePlaceHolderByTemplate:[InsertNewPrimaryKeyTextHere]WithDetailsSpec(string Template:[InsertNewPrimaryKeyCamelCaseTextHere])
        {
            Query.Where(x => x.Template:[InsertNewPrimaryKeyTextHere] == Template:[InsertNewPrimaryKeyCamelCaseTextHere]);
        }
    }
}
