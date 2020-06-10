using MediatR;
using ProjectNamePlaceHolder.WebAPI.Models;

namespace ProjectNamePlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemRequest : IRequest<MainModulePlaceHolderModel>
    {
        public int Id { get; set; }
    }
}
