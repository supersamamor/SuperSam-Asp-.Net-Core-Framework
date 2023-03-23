using ProjectNamePlaceHolder.Services.Application.InventoryUnits.DTOs;
using ProjectNamePlaceHolder.Services.Application.InventoryUnits.Queries.GetInventoryUnitsByProject;
using ProjectNamePlaceHolder.Services.Application.InventoryUnits.Queries.GetInventoryUnitsByProjectPaged;
using Cti.Core.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Services.API.Controllers
{
    public class InventoryUnitsController : ApiControllerBase
    {
        [HttpGet("ByProject/{projectCode}")]
        public async Task<IEnumerable<InventoryUnitDto>> GetByProjects(string projectCode)
        {
            var results = await Mediator.Send(new GetInventoryUnitsByProjectQuery() { ProjectCode = projectCode });
            return results;
        }

        [HttpPost("ByProject/Paged")]
        public async Task<PaginatedList<InventoryUnitDto>> GetByProjectsPaged(GetInventoryUnitsByProjectPagedQuery request)
        {
            var results = await Mediator.Send(request);
            return results;
        }
    }
}
