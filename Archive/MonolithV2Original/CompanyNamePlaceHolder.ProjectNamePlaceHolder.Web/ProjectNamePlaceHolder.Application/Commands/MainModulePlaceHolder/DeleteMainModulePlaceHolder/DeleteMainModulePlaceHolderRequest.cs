using MediatR;

namespace ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder
{
    public class DeleteMainModulePlaceHolderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
