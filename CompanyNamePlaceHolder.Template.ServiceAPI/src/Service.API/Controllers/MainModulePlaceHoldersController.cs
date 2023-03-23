using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.DTOs;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByProject;
using ProjectNamePlaceHolder.Services.Application.MainModulePlaceHolders.Queries.GetMainModulePlaceHoldersByProjectPaged;
using Cti.Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.API.Controllers
{
    public class MainModulePlaceHoldersController : ApiControllerBase
    {
        [HttpGet("ByProject/{projectCode}")]
        public async Task<IEnumerable<MainModulePlaceHolderDto>> GetByProjects(string projectCode)
        {
            var results = await Mediator.Send(new GetMainModulePlaceHoldersByProjectQuery() { ProjectCode = projectCode });
            return results;
        }

        [HttpPost("ByProject/Paged")]
        public async Task<PaginatedList<MainModulePlaceHolderDto>> GetByProjectsPaged(GetMainModulePlaceHoldersByProjectPagedQuery request)
        {
            var results = await Mediator.Send(request);
            return results;
        }
    }
}
