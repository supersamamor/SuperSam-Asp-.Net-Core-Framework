using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Identity.WebAPI.Models;
using Identity.WebAPI.Queries.GetRoleList;

namespace Identity.WebAPI.Controllers
{
    [Route("IdentityAPI/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RoleController : ControllerBase
    {      
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public RoleController(IMediator mediator, ILogger<RoleController> logger)
        {        
            _mediator = mediator;
            _logger = logger;  
        }

        [HttpGet("CurrentRoles")]
        public async Task<ActionResult<CustomPagedList<RoleModel>>> GetCurrentRoleListAsync(int userId, string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}",
                nameof(GetCurrentRoleListAsync), pageIndex, pageSize);
            try
            {
                var request = new GetRoleListRequest
                {
                    SearchKey = searchKey,
                    OrderBy = orderBy,
                    SortBy = sortBy,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    FilterBy = "CurrentRoles",
                    UserId = userId
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(GetCurrentRoleListAsync), e, _logger);
                return BadRequest(problem);
            }
        }
        [HttpGet("AvailableRoles")]
        public async Task<ActionResult<CustomPagedList<RoleModel>>> GetAvailableRoleListAsync(int userId, string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}",
                nameof(GetAvailableRoleListAsync), pageIndex, pageSize);
            try
            {
                var request = new GetRoleListRequest
                {
                    SearchKey = searchKey,
                    OrderBy = orderBy,
                    SortBy = sortBy,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    FilterBy = "AvailableRoles",
                    UserId = userId
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(GetAvailableRoleListAsync), e, _logger);
                return BadRequest(problem);
            }
        }
    }
}
