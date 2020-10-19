using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder
{
    public class AddMainModulePlaceHolderRequest : IRequest
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
