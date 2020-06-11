using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem
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
            var templateCore = await _repository.GetItemAsync(request.Id);
            return _mapper.Map<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(templateCore); ;
        }
    }
}
