using MediatR;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemByIdRequest : IRequest<MainModulePlaceHolderModel>
    {
        public string MainModulePlaceHolderId { get; set; }
    }
}
