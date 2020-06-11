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
            var templateCore = _mapper.Map<MainModulePlaceHolderModel, Core.Models.MainModulePlaceHolder>(request.MainModulePlaceHolder);        
            templateCore.SetCreatedInformation(request.Username);
            var template = await _repository.SaveAsync(templateCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(template); ;
        }
    }
}
