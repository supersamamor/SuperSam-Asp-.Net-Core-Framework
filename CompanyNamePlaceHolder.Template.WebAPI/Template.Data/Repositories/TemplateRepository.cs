using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Data.Repositories
{
    public class TemplateRepository
    {
        private readonly TemplateContext _context;
        private readonly IMapper _mapper;
        public TemplateRepository(TemplateContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }    

        public async Task<Data.Models.Template> SaveAsync(Core.Models.Template templateCore) {
            var template = _mapper.Map<Core.Models.Template, Models.Template>(templateCore);
            if (template.Id == 0)
            {
                await _context.Template.AddAsync(template);
            }
            else {
                _context.Entry(template).State = EntityState.Modified;
            }   
            return template;
        }

        public void Delete(Core.Models.Template templateCore)
        {
            var template = _mapper.Map<Core.Models.Template, Models.Template>(templateCore);
            _context.Template.Remove(template);
        }

        public async Task<Core.Models.Template> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.Template, Core.Models.Template>(await _context.Template.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
