using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Data.Repositories;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Commands.UpdateUser
{  
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        public UpdateUserRequestHandler(UserRepository repository, IdentityContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<UserModel> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.User.Id);
            templateCore.UpdateFrom(request.User.FullName);
            templateCore.SetUpdatedInformation(request.Username);
            var template = await _repository.SaveAsync(templateCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.ProjectNamePlaceHolderUser, UserModel>(template); ;
        }      
    }
}
