using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Data.Repositories;

namespace ProjectNamePlaceHolder.Web.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder
{  
    public class DeleteMainModulePlaceHolderRequestHandler : AsyncRequestHandler<DeleteMainModulePlaceHolderRequest>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;     
        public DeleteMainModulePlaceHolderRequestHandler(MainModulePlaceHolderRepository repository, ProjectNamePlaceHolderContext context) 
        {
            _repository = repository;
            _context = context;          
        }

        protected override async Task Handle(DeleteMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemAsync(request.Id);
            _repository.Delete(mainModulePlaceHolderCore);
            await _context.SaveChangesAsync();
        }

        public async Task HandleAsync(DeleteMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}
