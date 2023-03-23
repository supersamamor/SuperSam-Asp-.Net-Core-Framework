using Ardalis.Specification;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.DTOs;
using ProjectNamePlaceHolder.Services.Domain.Entities;
using Cti.Core.Application.Common.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByProject
{
    public class GetMainModulePlaceHoldersByProjectQuery : IRequest<IEnumerable<MainModulePlaceHolderDto>>
    {
        public string ProjectCode { get; set; } = "0000000001";
    }

    public class GetMainModulePlaceHoldersByProjectQueryHandler : IRequestHandler<GetMainModulePlaceHoldersByProjectQuery, IEnumerable<MainModulePlaceHolderDto>>
    {
        private readonly IRepository<MainModulePlaceHolder> _mainModulePlaceHolderRepository;

        public GetMainModulePlaceHoldersByProjectQueryHandler(IRepository<MainModulePlaceHolder> mainModulePlaceHolderRepository)
        {
            _mainModulePlaceHolderRepository = mainModulePlaceHolderRepository;
        }

        public async Task<IEnumerable<MainModulePlaceHolderDto>> Handle(GetMainModulePlaceHoldersByProjectQuery request, CancellationToken cancellationToken)
        {
            var spec = new InventoryByProjectWithDetailsSpec(request.ProjectCode);
            var unitsDto = await _mainModulePlaceHolderRepository.ListAsync(spec);
            return unitsDto;
        }
    }

    public class InventoryByProjectWithDetailsSpec : Specification<MainModulePlaceHolder, MainModulePlaceHolderDto>
    {
        public InventoryByProjectWithDetailsSpec(string projectCode)
        {
            Query.Where(x => x.ProjectCode == projectCode);
        }
    }
}
