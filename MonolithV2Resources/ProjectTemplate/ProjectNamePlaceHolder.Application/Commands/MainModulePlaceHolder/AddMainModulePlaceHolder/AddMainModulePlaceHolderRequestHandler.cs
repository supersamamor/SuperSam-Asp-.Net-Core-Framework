using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder
{  
    public class AddMainModulePlaceHolderRequestHandler : IRequestHandler<AddMainModulePlaceHolderRequest, MainModulePlaceHolderModel>
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

        public async Task<MainModulePlaceHolderModel> Handle(AddMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = _mapper.Map<MainModulePlaceHolderModel, Core.Models.MainModulePlaceHolder>(request.MainModulePlaceHolder);
            mainModulePlaceHolderCore.SetCreatedInformation(request.Username);
            var mainModulePlaceHolderData = await _repository.SaveAsync(mainModulePlaceHolderCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolderData);
        }  
    }
}
