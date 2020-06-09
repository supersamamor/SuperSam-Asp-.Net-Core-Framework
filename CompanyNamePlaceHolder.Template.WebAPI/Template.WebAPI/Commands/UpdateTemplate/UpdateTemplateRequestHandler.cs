using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Template.Data;
using Template.Data.Repositories;
using Template.WebAPI.Models;

namespace Template.WebAPI.Commands.UpdateTemplate
{  
    public class UpdateTemplateRequestHandler : IRequestHandler<UpdateTemplateRequest, TemplateModel>
    {
        private readonly TemplateRepository _repository;
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;
        public UpdateTemplateRequestHandler(TemplateRepository repository, TemplateContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<TemplateModel> Handle(UpdateTemplateRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.Template.Id);
            templateCore.UpdateFrom(request.Template.Name);
            templateCore.SetUpdatedInformation(request.Username);
            var template = await _repository.SaveAsync(templateCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.Template, TemplateModel>(template); ;
        }      
    }
}
