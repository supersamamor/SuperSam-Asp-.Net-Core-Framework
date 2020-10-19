using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.UpdateMainModulePlaceHolder
{
    public class UpdateMainModulePlaceHolderRequest : IRequest
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
