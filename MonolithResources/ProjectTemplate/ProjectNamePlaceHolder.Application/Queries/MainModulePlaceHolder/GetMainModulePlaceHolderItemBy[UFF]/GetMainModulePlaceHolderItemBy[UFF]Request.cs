using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemBy[UFF]
{
    public class GetMainModulePlaceHolderItemBy[UFF]Request : IRequest<MainModulePlaceHolderModel>
    {
        public string MainModulePlaceHolder[UFF] { get; set; }
    }
}
