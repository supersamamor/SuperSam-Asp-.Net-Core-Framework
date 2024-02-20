using MediatR;

namespace ProjectNamePlaceHolder.Web.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder
{
    public class DeleteMainModulePlaceHolderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
