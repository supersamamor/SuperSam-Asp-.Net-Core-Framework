using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Commands.User.ActivateUser
{  
    public class ActivateUserRequestHandler : IRequestHandler<ActivateUserRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public ActivateUserRequestHandler(UserRepository repository, ProjectNamePlaceHolderContext context,
            MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();           
        }
        public async Task<UserModel> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
        {
            using (var transaction = _context.Database.BeginTransaction()) 
            {
                var userCore = await _repository.GetItemAsync(request.Id);           
                userCore.SetUpdatedInformation(request.Username);             
                userCore.Identity.ActivateUser();
                var user = await _repository.SaveAsync(userCore);            
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return _mapper.Map<Data.Models.ProjectNamePlaceHolderUser, UserModel>(user);
            }         
        }      
    }
}
