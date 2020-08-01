using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Data.Repositories;

namespace ProjectNamePlaceHolder.Web.Commands.UpdateMainModulePlaceHolder
{  
    public class UpdateMainModulePlaceHolderRequestHandler : AsyncRequestHandler<UpdateMainModulePlaceHolderRequest>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public UpdateMainModulePlaceHolderRequestHandler(MainModulePlaceHolderRepository repository, ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        protected override async Task Handle(UpdateMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = await _repository.GetItemAsync(request.MainModulePlaceHolder.Id);
            mainModulePlaceHolderCore.UpdateFrom(request.MainModulePlaceHolder.Name);
            mainModulePlaceHolderCore.SetUpdatedInformation(request.Username);
            await _repository.SaveAsync(mainModulePlaceHolderCore);
            await _context.SaveChangesAsync();          
        }

        public async Task HandleAsync(UpdateMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            await this.Handle(request, cancellationToken);
        }
    }
}
