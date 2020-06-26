using MediatR;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Commands.AddMainModulePlaceHolder
{
    public class AddMainModulePlaceHolderRequest : IRequest
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }
        public string MainModulePlaceHolderId { get; set; }
    }
}
