using MediatR;
using ProjectNamePlaceHolder.WebAPI.Models;

namespace ProjectNamePlaceHolder.WebAPI.Commands.UpdateMainModulePlaceHolder
{
    public class UpdateMainModulePlaceHolderRequest : IRequest<MainModulePlaceHolderModel>
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
