using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Template.Data.Repositories;
using Template.WebAPI.Models;

namespace Template.WebAPI.Queries.GetTemplateItem
{
    public class GetTemplateItemRequestHandler : IRequestHandler<GetTemplateItemRequest, TemplateModel>
    {
        private readonly TemplateRepository _repository;
        private readonly IMapper _mapper;
        public GetTemplateItemRequestHandler(TemplateRepository repository, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<TemplateModel> Handle(GetTemplateItemRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.Id);
            return _mapper.Map<Core.Models.Template, TemplateModel>(templateCore); ;
        }
    }
}
