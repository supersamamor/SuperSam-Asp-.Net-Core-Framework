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

        public async Task<Data.Models.ProjectNamePlaceHolderUser> SaveAsync(Core.Models.User templateCore) {
            var template = _mapper.Map<Core.Models.User, Models.ProjectNamePlaceHolderUser>(templateCore);
            if (template.Id == 0)
            {
                await _context.ProjectNamePlaceHolderUser.AddAsync(template);
            }
            else {
                _context.Entry(template).State = EntityState.Modified;
            }   
            return template;
        }

        public void Delete(Core.Models.User templateCore)
        {
            var template = _mapper.Map<Core.Models.User, Models.ProjectNamePlaceHolderUser>(templateCore);
            _context.ProjectNamePlaceHolderUser.Remove(template);
        }

        public async Task<Core.Models.User> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.ProjectNamePlaceHolderUser, Core.Models.User>(await _context.ProjectNamePlaceHolderUser.Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
