using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SubComponentPlaceHolder.Data;
using SubComponentPlaceHolder.Data.Repositories;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Commands.UpdateMainModulePlaceHolder
{  
    public class UpdateMainModulePlaceHolderRequestHandler : IRequestHandler<UpdateMainModulePlaceHolderRequest, MainModulePlaceHolderModel>
    {
        private readonly MainModulePlaceHolderRepository _repository;
        private readonly SubComponentPlaceHolderContext _context;
        private readonly IMapper _mapper;
        public UpdateMainModulePlaceHolderRequestHandler(MainModulePlaceHolderRepository repository, SubComponentPlaceHolderContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<MainModulePlaceHolderModel> Handle(UpdateMainModulePlaceHolderRequest request, CancellationToken cancellationToken)
        {
            var templateCore = await _repository.GetItemAsync(request.MainModulePlaceHolder.Id);
            templateCore.UpdateFrom(request.MainModulePlaceHolder.Name);
            templateCore.SetUpdatedInformation(request.Username);
            var template = await _repository.SaveAsync(templateCore);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.MainModulePlaceHolder, MainModulePlaceHolderModel>(template); ;
        }      
    }
}
