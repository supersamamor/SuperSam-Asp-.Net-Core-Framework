using MediatR;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemByCode
{
    public class GetMainModulePlaceHolderItemByCodeRequest : IRequest<MainModulePlaceHolderModel>
    {
        public string MainModulePlaceHolderCode { get; set; }
    }
}
