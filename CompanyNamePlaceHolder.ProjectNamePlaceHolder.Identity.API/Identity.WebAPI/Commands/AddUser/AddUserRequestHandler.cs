using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Data.Repositories;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Commands.AddUser
{  
    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        public AddUserRequestHandler(UserRepository repository, IdentityContext context, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<UserModel> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var userCore = _mapper.Map<UserModel, Core.Models.ProjectNamePlaceHolderUser>(request.User);        
            userCore.SetCreatedInformation(request.Username);
            var user = await _repository.SaveAsync(userCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.ProjectNamePlaceHolderUser, UserModel>(user); ;
        }
    }
}
