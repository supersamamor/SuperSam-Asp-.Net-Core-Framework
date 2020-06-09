using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Template.Data;
using Template.Data.Repositories;

namespace Template.WebAPI.Commands.DeleteTemplate
{  
    public class DeleteTemplateRequestHandler : AsyncRequestHandler<DeleteTemplateRequest>
    {
        private readonly TemplateRepository _repository;
        private readonly TemplateContext _context;     
        public DeleteTemplateRequestHandler(TemplateRepository repository, TemplateContext context) 
        {
            _repository = repository;
            _context = context;          
        }

        protected override async Task Handle(DeleteTemplateRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.Id);
            _repository.Delete(templateCore);
            await _context.SaveChangesAsync();
        }

        public async Task HandleAsync(DeleteTemplateRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}
