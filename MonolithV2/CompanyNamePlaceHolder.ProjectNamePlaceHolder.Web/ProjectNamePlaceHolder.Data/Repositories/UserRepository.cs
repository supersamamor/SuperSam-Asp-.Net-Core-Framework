using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Data.Repositories
{
    public class UserRepository
    {
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapperConfig.CreateMapper();
            _userManager = userManager;
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

        public async Task<IList<string>> GetUserRoles(int id)
        {
            var identity = (await _context.ProjectNamePlaceHolderUser.Include(l => l.Identity).Where(l => l.Id == id).AsNoTracking().FirstOrDefaultAsync())?.Identity;
            return await _userManager.GetRolesAsync(identity);      
        }
    }     
}
