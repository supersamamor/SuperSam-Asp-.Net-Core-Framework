using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MediatR;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.UpdateMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderList;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem;
using ProjectNamePlaceHolder.Application.Models;
using ProjectNamePlaceHolder.Data;
using ProjectNamePlaceHolder.Application.Exception;

namespace ProjectNamePlaceHolder.Application.ApplicationServices.MainModulePlaceHolder
{
    public class MainModulePlaceHolderService  : BaseApplicationService
    {
        public MainModulePlaceHolderService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {            
        }

        public async Task<CustomPagedList<MainModulePlaceHolderModel>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {          
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new GetMainModulePlaceHolderListRequest
            {
                SearchKey = searchKey,
                OrderBy = orderBy,
                SortBy = sortBy,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return await _mediator.Send(request);          
        }

        public async Task<MainModulePlaceHolderModel> GetMainModulePlaceHolderItemAsync(int id)
        {
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new GetMainModulePlaceHolderItemRequest
            {
                Id = id
            };
            return await _mediator.Send(request);
        }

        public async Task<MainModulePlaceHolderModel> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder)
        {
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new UpdateMainModulePlaceHolderRequest
            {
                MainModulePlaceHolder = mainModulePlaceHolder,
                Username = _userName
            };
            await _mediator.Send(request);

            var updatedMainModulePlaceHolderRequest = new GetMainModulePlaceHolderItemRequest
            {
                Id = mainModulePlaceHolder.Id
            };
            return await _mediator.Send(updatedMainModulePlaceHolderRequest);
        }

        public async Task<MainModulePlaceHolderModel> SaveMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder)
        {
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new AddMainModulePlaceHolderRequest
            {
                MainModulePlaceHolder = mainModulePlaceHolder,
                Username = _userName
            };
            return await _mediator.Send(request);         
        }

        public async Task DeleteMainModulePlaceHolderAsync(int id)
        {
            if (!_claims.IsInRole(Roles.ADMIN))
            {
                throw new UnAuthorizedException();
            }
            var request = new DeleteMainModulePlaceHolderRequest
            {
                Id = id
            };
            await _mediator.Send(request);
        }
    }
}
