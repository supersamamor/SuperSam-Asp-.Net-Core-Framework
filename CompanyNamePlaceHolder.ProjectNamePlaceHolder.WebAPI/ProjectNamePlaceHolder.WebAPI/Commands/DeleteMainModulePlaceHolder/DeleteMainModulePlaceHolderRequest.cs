using MediatR;

namespace ProjectNamePlaceHolder.WebAPI.Commands.DeleteMainModulePlaceHolder
{
    public class DeleteMainModulePlaceHolderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
