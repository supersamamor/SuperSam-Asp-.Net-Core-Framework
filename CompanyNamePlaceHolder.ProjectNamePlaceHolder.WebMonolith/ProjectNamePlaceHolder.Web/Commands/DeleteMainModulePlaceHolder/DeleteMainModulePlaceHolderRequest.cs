using MediatR;

namespace ProjectNamePlaceHolder.Web.Commands.DeleteMainModulePlaceHolder
{
    public class DeleteMainModulePlaceHolderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
