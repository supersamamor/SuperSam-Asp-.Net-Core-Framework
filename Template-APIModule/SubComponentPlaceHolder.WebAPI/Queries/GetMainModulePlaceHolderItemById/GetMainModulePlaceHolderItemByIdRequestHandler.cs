using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemByIdRequestHandler : IRequestHandler<GetMainModulePlaceHolderItemByIdRequest, MainModulePlaceHolderModel>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderItemByIdRequestHandler(MainModulePlaceHolderRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<MainModulePlaceHolderModel> Handle(GetMainModulePlaceHolderItemByIdRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemByIdAsync(request.MainModulePlaceHolderId);
            return _mapper.Map<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolderCore); 
        }
    }
}
