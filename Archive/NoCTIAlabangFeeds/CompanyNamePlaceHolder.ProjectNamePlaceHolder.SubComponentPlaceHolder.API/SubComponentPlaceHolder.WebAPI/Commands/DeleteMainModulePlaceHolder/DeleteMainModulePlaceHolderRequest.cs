using MediatR;

namespace SubComponentPlaceHolder.WebAPI.Commands.DeleteMainModulePlaceHolder
{
    public class DeleteMainModulePlaceHolderRequest : IRequest
    {
        public int Id { get; set; }
    }
}
