using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Commands.AddMainModulePlaceHolder
{  
    public class AddMainModulePlaceHolderRequestHandler : AsyncRequestHandler<AddMainModulePlaceHolderRequest>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly SubComponentPlaceHolderContext _context;
        private readonly IMapper _mapper;
        public AddMainModulePlaceHolderRequestHandler(MainModulePlaceHolderRepository repository, SubComponentPlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        
        protected override async Task Handle(AddMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = _mapper.Map<MainModulePlaceHolderModel, Core.Models.MainModulePlaceHolder>(request.MainModulePlaceHolder);
            mainModulePlaceHolderCore.SetMainModulePlaceHolderId(request.MainModulePlaceHolderId);
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
