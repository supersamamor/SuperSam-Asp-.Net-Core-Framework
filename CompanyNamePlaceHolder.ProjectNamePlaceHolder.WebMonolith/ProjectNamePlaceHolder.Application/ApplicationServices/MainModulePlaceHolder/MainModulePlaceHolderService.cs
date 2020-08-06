using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using MediatR;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.UpdateMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderList;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemByCode;

namespace ProjectNamePlaceHolder.Application.ApplicationServices.MainModulePlaceHolder
{
    public class MainModulePlaceHolderService  : BaseApplicationService
    {
        public MainModulePlaceHolderService(IMediator mediator, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContext) 
            : base(mediator, userManager, httpContext)
        {            
        }

        public async Task<IPagedList<MainModulePlaceHolderModel>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
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
            var request = new GetMainModulePlaceHolderItemRequest
            {
                Id = id
            };
            return await _mediator.Send(request);
        }

        public async Task<MainModulePlaceHolderModel> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel mainModulePlaceHolder)
        {          
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
            var request = new AddMainModulePlaceHolderRequest
            {
                MainModulePlaceHolder = mainModulePlaceHolder,
                Username = _userName
            };
            await _mediator.Send(request);

            var savedMainModulePlaceHolderRequest = new GetMainModulePlaceHolderItemByCodeRequest
            {
                MainModulePlaceHolderCode = mainModulePlaceHolder.Code
            };
            return await _mediator.Send(savedMainModulePlaceHolderRequest);
        }

        public async Task DeleteMainModulePlaceHolderAsync(int id)
        {
            var request = new DeleteMainModulePlaceHolderRequest
            {
                Id = id
            };
            await _mediator.Send(request);
        }
    }
}
