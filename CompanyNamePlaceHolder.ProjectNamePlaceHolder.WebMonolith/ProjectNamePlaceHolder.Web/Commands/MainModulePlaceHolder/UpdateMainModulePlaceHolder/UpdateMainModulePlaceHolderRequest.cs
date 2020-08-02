using MediatR;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Commands.MainModulePlaceHolder.UpdateMainModulePlaceHolder
{
    public class UpdateMainModulePlaceHolderRequest : IRequest
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
