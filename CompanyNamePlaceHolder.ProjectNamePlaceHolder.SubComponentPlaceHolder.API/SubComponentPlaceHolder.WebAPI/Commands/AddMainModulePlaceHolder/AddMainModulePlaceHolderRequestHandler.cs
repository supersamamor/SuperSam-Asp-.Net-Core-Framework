using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Commands.AddMainModulePlaceHolder
{  
    public class AddMainModulePlaceHolderRequestHandler : IRequestHandler<AddMainModulePlaceHolderRequest, MainModulePlaceHolderModel>
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

        public async Task<MainModulePlaceHolderModel> Handle(AddMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var mainModulePlaceHolderCore = _mapper.Map<MainModulePlaceHolderModel, Core.Models.MainModulePlaceHolder>(request.MainModulePlaceHolder);        
            mainModulePlaceHolderCore.SetCreatedInformation(request.Username);
            var mainModulePlaceHolder = await _repository.SaveAsync(mainModulePlaceHolderCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(mainModulePlaceHolder); ;
        }
    }
}
