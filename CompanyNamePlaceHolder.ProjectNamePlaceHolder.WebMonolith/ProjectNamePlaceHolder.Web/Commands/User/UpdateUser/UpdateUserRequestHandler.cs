using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectNamePlaceHolder.Web.Models.User;
using ProjectNamePlaceHolder.Data.Repositories;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Web.Models.Role;

namespace ProjectNamePlaceHolder.Web.Commands.User.UpdateUser
{  
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, UserModel>
    {
        private readonly UserRepository _repository;
        private readonly ProjectNamePlaceHolderContext _context;
        private readonly IMapper _mapper;
        public UpdateUserRequestHandler(UserRepository repository, ProjectNamePlaceHolderContext context, MapperConfiguration mapperConfig) 
        {
            _repository = repository;
            _context = context;
            _mapper = mapperConfig.CreateMapper();
        }
        public async Task<UserModel> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var userCore = await _repository.GetItemAsync(request.User.Id);
            userCore.UpdateFrom(request.User.FullName, request.User.IdentityEmail);
            userCore.SetUpdatedInformation(request.Username);
            var user = await _repository.SaveAsync(userCore);
            await UpdateUserRoleAsync(userCore.Identity.Id, request.User.UserRoles);
            await _context.SaveChangesAsync();
            return _mapper.Map<Data.Models.ProjectNamePlaceHolderUser, UserModel>(user); ;
        }   
        private async Task UpdateUserRoleAsync(string identityId, IList<RoleModel> userRoles)
        {
            var roleIds = userRoles.Select(l => l.Id).ToList();
            // Delete roles
            var rolesToRemove = await _context.UserRoles
                    .Where(l => !roleIds.Contains(l.RoleId) && l.UserId == identityId)
                    .AsNoTracking().ToListAsync();

            foreach (var userRoleToRemove in rolesToRemove)
            {              
                _context.UserRoles.Remove(userRoleToRemove);
            }

            // Update and Insert roles
            foreach (var roleIdToUpsert in roleIds)
            {
                var userRole = _context.UserRoles
                    .Where(c => c.UserId == identityId && c.RoleId == roleIdToUpsert)
                    .SingleOrDefault();
               
                if (userRole != null)
                    // Update 
                    _context.Entry(userRole).State = EntityState.Modified;             
                else
                {
                    // Insert 
                    var newUserRole = new IdentityUserRole<string>
                    {
                        UserId = identityId,
                        RoleId = roleIdToUpsert
                    };
                    _context.Add(newUserRole);
                }
            }
        }
    }
}
