using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Commands.AddMainModulePlaceHolder
{  
    public class AddMainModulePlaceHolderRequestHandler : AsyncRequestHandler<AddMainModulePlaceHolderRequest>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public AddMainModulePlaceHolderRequestHandler(MainModulePlaceHolderRepository repository, ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        
        protected override async Task Handle(AddMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = _mapper.Map<MainModulePlaceHolderModel, Core.Models.MainModulePlaceHolder>(request.MainModulePlaceHolder);        
            mainModulePlaceHolderCore.SetCreatedInformation(request.Username);
            await _repository.SaveAsync(mainModulePlaceHolderCore);
            await _context.SaveChangesAsync();
        }

        public async Task HandleAsync(AddMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}
