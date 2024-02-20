using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder
{
    public class AddMainModulePlaceHolderRequest : IRequest<MainModulePlaceHolderModel>
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
