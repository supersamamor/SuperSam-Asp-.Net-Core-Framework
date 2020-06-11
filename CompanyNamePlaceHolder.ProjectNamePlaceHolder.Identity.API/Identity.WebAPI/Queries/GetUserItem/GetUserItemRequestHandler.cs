using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data.Repositories;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Queries.GetUserItem
{
    public class GetUserItemRequestHandler : IRequestHandler<GetUserItemRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;
        public GetUserItemRequestHandler(UserRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<UserModel> Handle(GetUserItemRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.Id);
            return _mapper.Map<Core.Models.User, UserModel>(templateCore); ;
        }
    }
}
