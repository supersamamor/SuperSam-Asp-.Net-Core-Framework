using MediatR;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemRequest : IRequest<MainModulePlaceHolderModel>
    {
        public int Id { get; set; }
    }
}
