using MediatR;
using ProjectNamePlaceHolder.WebAPI.Models;

namespace ProjectNamePlaceHolder.WebAPI.Commands.AddMainModulePlaceHolder
{
    public class AddMainModulePlaceHolderRequest : IRequest<MainModulePlaceHolderModel>
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }
    }
}
