using MediatR;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemRequest : IRequest<MainModulePlaceHolderModel>
    {
        public int Id { get; set; }
    }
}
