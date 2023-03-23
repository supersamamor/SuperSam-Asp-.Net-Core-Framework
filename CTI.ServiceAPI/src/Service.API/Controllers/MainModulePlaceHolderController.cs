using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.DTOs;
using Cti.Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersByPrimaryKey;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolder.Queries.GetMainModulePlaceHoldersPaged;

namespace ProjectNamePlaceHolder.Services.API.Controllers
{
    public class MainModulePlaceHolderController : ApiControllerBase
    {
        [HttpGet("{primaryKey}")]
        public async Task<MainModulePlaceHolderDto> GetAsync(string primaryKey)
        {
            var results = await Mediator.Send(new GetMainModulePlaceHolderByPrimaryKeyQuery() { PrimaryKey = primaryKey });
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
