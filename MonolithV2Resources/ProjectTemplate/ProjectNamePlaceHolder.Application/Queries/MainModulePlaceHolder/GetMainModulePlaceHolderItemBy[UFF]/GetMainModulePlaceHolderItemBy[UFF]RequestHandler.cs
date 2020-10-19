using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemBy[UFF]
{
    public class GetMainModulePlaceHolderItemBy[UFF]RequestHandler : IRequestHandler<GetMainModulePlaceHolderItemBy[UFF]Request, MainModulePlaceHolderModel>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly IMapper _mapper;
        public GetMainModulePlaceHolderItemBy[UFF]RequestHandler(MainModulePlaceHolderRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<MainModulePlaceHolderModel> Handle(GetMainModulePlaceHolderItemBy[UFF]Request request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemBy[UFF]Async(request.MainModulePlaceHolder[UFF]);
            return _mapper.Map<Core.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolderCore); 
        }
    }
}
