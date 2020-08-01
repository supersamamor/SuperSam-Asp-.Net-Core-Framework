using MediatR;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Commands.UpdateMainModulePlaceHolder
{
    public class UpdateMainModulePlaceHolderRequest : IRequest
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
