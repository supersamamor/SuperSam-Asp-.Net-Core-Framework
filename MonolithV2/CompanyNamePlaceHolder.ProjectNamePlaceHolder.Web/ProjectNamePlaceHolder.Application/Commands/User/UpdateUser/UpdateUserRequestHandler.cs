using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectNamePlaceHolder.Application.Models.User;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectNamePlaceHolder.Application.Commands.User.UpdateUser
{  
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateUserRequestHandler(UserRepository repository, ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig,
             UserManager<IdentityUser> userManager) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
            _userManager = userManager;
        }
        public async Task<UserModel> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userCore = await _repository.GetItemAsync(request.User.Id);
            userCore.UpdateFrom(request.User.FullName, request.User.IdentityEmail);
            userCore.SetUpdatedInformation(request.Username);
            var user = await _repository.SaveAsync(userCore);
            await UpdateUserRoleAsync(userCore.Identity.Id, request.User.Roles);
            await _context.SaveChangesAsync();         
            var userModel = _mapper.Map<Data.Models.ProjectNamePlaceHolderUser, UserModel>(user);
            userModel.Roles = await _repository.GetUserRoles(user.Id);
            return userModel;
        }   
        private async Task UpdateUserRoleAsync(string identityId, IList<string> roles)
        {
            var identity = await _context.Users.Where(l => l.Id == identityId).FirstOrDefaultAsync();
            var result = IdentityResult.Success;
            var currentRoles = await _userManager.GetRolesAsync(identity);
            foreach (var item in currentRoles)
            {
                result = await _userManager.RemoveFromRoleAsync(identity, item);
            }
            if (roles?.Count > 0)
            {
                foreach (var role in roles)
                {
                    var isInRole = await _userManager.IsInRoleAsync(identity, role);
                    if (!isInRole)
                    {
                        result = await _userManager.AddToRoleAsync(identity, role);
                    }
                    if (!result.Succeeded)
                    {
                        break;
                    }
                }
            }          
        }
    }
}
