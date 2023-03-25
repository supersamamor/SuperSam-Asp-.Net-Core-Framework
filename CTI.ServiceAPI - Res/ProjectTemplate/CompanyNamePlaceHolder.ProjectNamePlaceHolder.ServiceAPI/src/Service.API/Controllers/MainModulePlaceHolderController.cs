using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersByTemplate[PK];
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersPaged;

namespace ProjectNamePlaceHolder.Services.API.Controllers
{
    public class MainModulePlaceHolderController : ApiControllerBase
    {
        [HttpGet("{Template[InsertNewPrimaryKeyCamelCaseTextHere]}")]
        public async Task<MainModulePlaceHolderDto> GetAsync(string Template[InsertNewPrimaryKeyCamelCaseTextHere])
        {
            var results = await Mediator.Send(new GetMainModulePlaceHolderByTemplate[PK]Query() { Template[PK] = Template[InsertNewPrimaryKeyCamelCaseTextHere] });
            return results;
        }

        [HttpGet]
        public async Task<PaginatedList<MainModulePlaceHolderDto>> GetAsync(GetMainModulePlaceHolderPagedQuery request)
        {
            var results = await Mediator.Send(request);
            return results;
        }
    }
}
