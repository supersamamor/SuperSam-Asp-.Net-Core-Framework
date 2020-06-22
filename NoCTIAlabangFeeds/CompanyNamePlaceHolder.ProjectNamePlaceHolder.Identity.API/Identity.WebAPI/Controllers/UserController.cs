using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Identity.WebAPI.Commands.UpdateUser;
using Identity.WebAPI.Models;
using Identity.WebAPI.Queries.GetUserItem;
using Identity.WebAPI.Queries.GetUserList;
using Identity.WebAPI.Commands.ActivateUser;
using System.Collections.Generic;

namespace Identity.WebAPI.Controllers
{
    [Route("IdentityAPI/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {      
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {        
            _mediator = mediator;
            _logger = logger;  
        } 

        [HttpGet]
        public async Task<ActionResult<CustomPagedList<UserModel>>> GetUserListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}", 
                nameof(GetUserListAsync), pageIndex, pageSize);
            try
            {                
                var request = new GetUserListRequest
                {
                    SearchKey = searchKey,
                    OrderBy = orderBy,
                    SortBy = sortBy,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                return await _mediator.Send(request);            
            }
            catch (Exception e)
            {             
                var problem = new CustomValidationProblemDetails(nameof(GetUserListAsync), e, _logger);
                return BadRequest(problem); 
            }                
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserItemAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(GetUserItemAsync), id);
            try
            {
                var request = new GetUserItemRequest
                {
                    Id = id
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(GetUserItemAsync), e, _logger);
                return BadRequest(problem);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UserModel>> UpdateUserAsync(UserModel user)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: User={User}", nameof(UpdateUserAsync), user);
            try
            {                
                var request = new UpdateUserRequest
                {
                    User = user,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(UpdateUserAsync), e, _logger);
                return BadRequest(problem);
            }
        }
        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<UserModel>> ActivateUserAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: UserId={UserId}", nameof(ActivateUserAsync), id);
            try
            {
                var request = new ActivateUserRequest
                {
                    Id = id,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(UpdateUserAsync), e, _logger);
                return BadRequest(problem);
            }
        }
    }
}
