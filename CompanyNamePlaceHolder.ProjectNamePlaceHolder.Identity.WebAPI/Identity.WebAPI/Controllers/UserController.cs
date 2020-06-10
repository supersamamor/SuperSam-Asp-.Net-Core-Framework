using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Identity.WebAPI.Commands.AddUser;
using Identity.WebAPI.Commands.DeleteUser;
using Identity.WebAPI.Commands.UpdateUser;
using Identity.WebAPI.Models;
using Identity.WebAPI.Queries.GetUserItem;
using Identity.WebAPI.Queries.GetUserList;
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
        public async Task<ActionResult<UserModel>> UpdateUserAsync(UserModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: User={User}", nameof(UpdateUserAsync), template);
            try
            {                
                var request = new UpdateUserRequest
                {
                    User = template,
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
      
        [HttpPost]
        public async Task<ActionResult<UserModel>> AddUserAsync(UserModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: User={User}", nameof(AddUserAsync), template);
            try
            {
                var request = new AddUserRequest
                {
                    User = template,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(AddUserAsync), e, _logger);
                return BadRequest(problem);
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(DeleteUserAsync), id);
            try
            {
                var request = new DeleteUserRequest
                {
                    Id = id
                };
                await _mediator.Send(request);
                return NoContent();
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(DeleteUserAsync), e, _logger);
                return BadRequest(problem);
            }
        }   
    }
}
