using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.DTOs;
using Cti.Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByPrimaryKey;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersPaged;

namespace ProjectNamePlaceHolder.Services.API.Controllers
{
    public class MainModulePlaceHolderController : ApiControllerBase
    {
        [HttpGet("{primaryKey}")]
        public async Task<MainModulePlaceHolderDto> GetAsync(string primaryKey)
        {
            var results = await Mediator.Send(new GetMainModulePlaceHoldersByPrimaryKeyQuery() { PrimaryKey = primaryKey });
            return results;
        }

        [HttpGet]
        public async Task<PaginatedList<MainModulePlaceHolderDto>> GetByProjectsPaged(GetMainModulePlaceHolderPagedQuery request)
        {
            var results = await Mediator.Send(request);
            return results;
        }
    }
}
