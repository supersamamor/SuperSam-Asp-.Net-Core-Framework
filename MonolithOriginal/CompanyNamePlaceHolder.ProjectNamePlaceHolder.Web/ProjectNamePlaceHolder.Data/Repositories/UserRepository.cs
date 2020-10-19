using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Data.Repositories
{
    public class UserRepository
    {
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }    

        public async Task<Data.Models.ProjectNamePlaceHolderUser> SaveAsync(Core.Models.ProjectNamePlaceHolderUser userCore) {
            var user = _mapper.Map<Core.Models.ProjectNamePlaceHolderUser, Models.ProjectNamePlaceHolderUser>(userCore);
            if (user.Id == 0)
            {
                await _context.ProjectNamePlaceHolderUser.AddAsync(user);
            }
            else {
                _context.Entry(user).State = EntityState.Modified;
                _context.Entry(user.Identity).State = EntityState.Modified;
            }   
            return user;
        }

        public void Delete(Core.Models.ProjectNamePlaceHolderUser userCore)
        {
            var user = _mapper.Map<Core.Models.ProjectNamePlaceHolderUser, Models.ProjectNamePlaceHolderUser>(userCore);
            _context.ProjectNamePlaceHolderUser.Remove(user);
        }

        public async Task<Core.Models.ProjectNamePlaceHolderUser> GetItemAsync(int id)
        {        
            return _mapper.Map<Models.ProjectNamePlaceHolderUser, Core.Models.ProjectNamePlaceHolderUser>
                (await _context.ProjectNamePlaceHolderUser.Include(l=>l.Identity).Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync());          
        }       
    }     
}
