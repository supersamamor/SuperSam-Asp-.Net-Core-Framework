using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Template.Data;
using Template.Data.Repositories;
using Template.WebAPI.Models;

namespace Template.WebAPI.Commands.AddTemplate
{  
    public class AddTemplateRequestHandler : IRequestHandler<AddTemplateRequest, TemplateModel>
    {
        private readonly TemplateRepository _repository;
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;
        public AddTemplateRequestHandler(TemplateRepository repository, TemplateContext context, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }

        public async Task<TemplateModel> Handle(AddTemplateRequest request, CancellationToken cancellationToken)
        {
            var templateCore = _mapper.Map<TemplateModel, Core.Models.Template>(request.Template);        
            templateCore.SetCreatedInformation(request.Username);
            var template = await _repository.SaveAsync(templateCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.Template, TemplateModel>(template); ;
        }
    }
}
