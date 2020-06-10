using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public class UserRepository
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        public UserRepository(IdentityContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }    

        public async Task<Data.Models.User> SaveAsync(Core.Models.User templateCore) {
            var template = _mapper.Map<Core.Models.User, Models.User>(templateCore);
            if (template.Id == null)
            {
                await _context.User.AddAsync(template);
            }
            else {
                _context.Entry(template).State = EntityState.Modified;
            }   
            return template;
        }

        public void Delete(Core.Models.User templateCore)
        {
            var template = _mapper.Map<Core.Models.User, Models.User>(templateCore);
            _context.User.Remove(template);
        }

        public async Task<Core.Models.User> GetItemAsync(string id)
        {        
            return _mapper.Map<Models.User, Core.Models.User>(await _context.User.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
