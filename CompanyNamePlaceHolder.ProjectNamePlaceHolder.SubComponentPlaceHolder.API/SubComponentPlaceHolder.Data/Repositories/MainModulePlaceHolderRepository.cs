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

        public async Task<Data.Models.MainModulePlaceHolder> SaveAsync(Core.Models.MainModulePlaceHolder templateCore) {
            var template = _mapper.Map<Core.Models.MainModulePlaceHolder, Models.MainModulePlaceHolder>(templateCore);
            if (template.Id == 0)
            {
                await _context.MainModulePlaceHolder.AddAsync(template);
            }
            else {
                _context.Entry(template).State = EntityState.Modified;
            }   
            return template;
        }

        public void Delete(Core.Models.MainModulePlaceHolder templateCore)
        {
            var template = _mapper.Map<Core.Models.MainModulePlaceHolder, Models.MainModulePlaceHolder>(templateCore);
            _context.MainModulePlaceHolder.Remove(template);
        }

        public async Task<Core.Models.MainModulePlaceHolder> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.MainModulePlaceHolder, Core.Models.MainModulePlaceHolder>(await _context.MainModulePlaceHolder.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
