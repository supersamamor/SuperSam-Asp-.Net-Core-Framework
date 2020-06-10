using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Identity.Data;
using Identity.Data.Repositories;

namespace Identity.WebAPI.Commands.DeleteUser
{  
    public class DeleteUserRequestHandler : AsyncRequestHandler<DeleteUserRequest>
    {
        private readonly UserRepository _repository;
        private readonly IdentityContext _context;     
        public DeleteUserRequestHandler(UserRepository repository, IdentityContext context) 
        {
            _repository = repository;
            _context = context;          
        }

        protected override async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.Id);
            _repository.Delete(templateCore);
            await _context.SaveChangesAsync();
        }

        public async Task HandleAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}
