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

        public async Task<Data.Models.ProjectNamePlaceHolderUser> SaveAsync(Core.Models.User userCore) {
            var user = _mapper.Map<Core.Models.User, Models.ProjectNamePlaceHolderUser>(userCore);
            if (user.Id == 0)
            {
                await _context.ProjectNamePlaceHolderUser.AddAsync(user);
            }
            else {
                _context.Entry(user).State = EntityState.Modified;
            }   
            return user;
        }

        public void Delete(Core.Models.User userCore)
        {
            var user = _mapper.Map<Core.Models.User, Models.ProjectNamePlaceHolderUser>(userCore);
            _context.ProjectNamePlaceHolderUser.Remove(user);
        }

        public async Task<Core.Models.User> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.ProjectNamePlaceHolderUser, Core.Models.User>
                (await _context.ProjectNamePlaceHolderUser.Include(l=>l.Identity).Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
