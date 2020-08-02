using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemRequestHandler : IRequestHandler<GetMainModulePlaceHolderItemRequest, MainModulePlaceHolderModel>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderItemRequestHandler(MainModulePlaceHolderRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<MainModulePlaceHolderModel> Handle(GetMainModulePlaceHolderItemRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemAsync(request.Id);
            return _mapper.Map<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolderCore); 
        }
    }
}
