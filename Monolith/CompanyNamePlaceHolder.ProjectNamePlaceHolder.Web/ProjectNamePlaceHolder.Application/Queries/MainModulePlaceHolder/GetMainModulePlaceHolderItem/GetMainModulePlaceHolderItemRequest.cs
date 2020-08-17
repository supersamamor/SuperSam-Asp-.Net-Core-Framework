using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem
{
    public class GetMainModulePlaceHolderItemRequest : IRequest<MainModulePlaceHolderModel>
    {
        public int Id { get; set; }
    }
}
