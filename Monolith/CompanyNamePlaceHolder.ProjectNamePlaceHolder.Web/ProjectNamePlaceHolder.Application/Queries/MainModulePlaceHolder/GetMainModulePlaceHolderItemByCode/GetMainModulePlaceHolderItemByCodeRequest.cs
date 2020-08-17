using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemByCode
{
    public class GetMainModulePlaceHolderItemByCodeRequest : IRequest<MainModulePlaceHolderModel>
    {
        public string MainModulePlaceHolderCode { get; set; }
    }
}
