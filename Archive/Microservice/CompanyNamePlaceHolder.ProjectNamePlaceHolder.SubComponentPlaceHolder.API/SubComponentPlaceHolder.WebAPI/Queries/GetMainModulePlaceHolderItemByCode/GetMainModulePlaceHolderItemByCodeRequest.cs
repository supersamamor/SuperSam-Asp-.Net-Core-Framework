using MediatR;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItemByCode
{
    public class GetMainModulePlaceHolderItemByCodeRequest : IRequest<MainModulePlaceHolderModel>
    {
        public string MainModulePlaceHolderCode { get; set; }
    }
}
