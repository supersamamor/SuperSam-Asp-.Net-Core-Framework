using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.WebAPI.Models;

namespace ProjectNamePlaceHolder.WebAPI.Commands.UpdateMainModulePlaceHolder
{  
    public class UpdateMainModulePlaceHolderRequestHandler : IRequestHandler<UpdateMainModulePlaceHolderRequest, MainModulePlaceHolderModel>
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
