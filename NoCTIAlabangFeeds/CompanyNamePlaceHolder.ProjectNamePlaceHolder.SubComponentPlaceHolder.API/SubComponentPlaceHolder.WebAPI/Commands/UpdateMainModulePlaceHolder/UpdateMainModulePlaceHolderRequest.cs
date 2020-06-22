using MediatR;
using SubComponentPlaceHolder.WebAPI.Models;

namespace SubComponentPlaceHolder.WebAPI.Commands.UpdateMainModulePlaceHolder
{
    public class UpdateMainModulePlaceHolderRequest : IRequest<MainModulePlaceHolderModel>
    {
        public MainModulePlaceHolderModel MainModulePlaceHolder { get; set; }
        public string Username { get; set; }        
    }
}
