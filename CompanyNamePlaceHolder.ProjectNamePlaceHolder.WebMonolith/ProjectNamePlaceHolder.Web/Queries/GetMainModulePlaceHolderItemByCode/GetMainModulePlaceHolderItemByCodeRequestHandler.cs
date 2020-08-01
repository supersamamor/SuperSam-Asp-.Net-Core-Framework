using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Queries.GetMainModulePlaceHolderItemByCode
{
    public class GetMainModulePlaceHolderItemByCodeRequestHandler : IRequestHandler<GetMainModulePlaceHolderItemByCodeRequest, MainModulePlaceHolderModel>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderItemByCodeRequestHandler(MainModulePlaceHolderRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<MainModulePlaceHolderModel> Handle(GetMainModulePlaceHolderItemByCodeRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemByCodeAsync(request.MainModulePlaceHolderCode);
            return _mapper.Map<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolderCore); 
        }
    }
}
