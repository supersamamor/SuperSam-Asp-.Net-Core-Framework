using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SubComponentPlaceHolder.Data.Repositories
{
    public class MainModulePlaceHolderRepository
    {
        private readonly SubComponentPlaceHolderContext _context;
        private readonly IMapper _mapper;
        public MainModulePlaceHolderRepository(SubComponentPlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }    

        public async Task<Data.Models.MainModulePlaceHolder> SaveAsync(Core.Models.MainModulePlaceHolder mainModulePlaceHolderCore) 
        {
            var mainModulePlaceHolder = _mapper.Map<Core.Models.MainModulePlaceHolder, Models.MainModulePlaceHolder>(mainModulePlaceHolderCore);
            if (mainModulePlaceHolder.Id == 0)
            {
                await _context.MainModulePlaceHolder.AddAsync(mainModulePlaceHolder);
            }
            else {
                _context.Entry(mainModulePlaceHolder).State = EntityState.Modified;
            }   
            return mainModulePlaceHolder;
        }

        public void Delete(Core.Models.MainModulePlaceHolder mainModulePlaceHolderCore)
        {
            var mainModulePlaceHolder = _mapper.Map<Core.Models.MainModulePlaceHolder, Models.MainModulePlaceHolder>(mainModulePlaceHolderCore);
            _context.MainModulePlaceHolder.Remove(mainModulePlaceHolder);
        }

        public async Task<Core.Models.MainModulePlaceHolder> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.MainModulePlaceHolder, Core.Models.MainModulePlaceHolder>(await _context.MainModulePlaceHolder.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }
        public async Task<Core.Models.MainModulePlaceHolder> GetItemByCodeAsync(string mainModulePlaceHolderCode)
        {
            return _mapper.Map<Models.MainModulePlaceHolder, Core.Models.MainModulePlaceHolder>(await _context.MainModulePlaceHolder.Where(l => l.Code == mainModulePlaceHolderCode).AsNoTracking().FirstOrDefaultAsync());
        }
    }     
}
