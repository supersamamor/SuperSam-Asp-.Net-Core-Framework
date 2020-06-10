using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.WebAPI.Commands.AddMainModulePlaceHolder;
using ProjectNamePlaceHolder.WebAPI.Commands.DeleteMainModulePlaceHolder;
using ProjectNamePlaceHolder.WebAPI.Commands.UpdateMainModulePlaceHolder;
using ProjectNamePlaceHolder.WebAPI.Models;
using ProjectNamePlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderItem;
using ProjectNamePlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderList;
namespace ProjectNamePlaceHolder.WebAPI.Controllers
{
    [Route("MainModulePlaceHolderAPI/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MainModulePlaceHolderController : ControllerBase
    {      
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public MainModulePlaceHolderController(IMediator mediator, ILogger<MainModulePlaceHolderController> logger)
        {        
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CustomPagedList<MainModulePlaceHolderModel>>> GetMainModulePlaceHolderListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}", 
                nameof(GetMainModulePlaceHolderListAsync), pageIndex, pageSize);
            try
            {                
                var request = new GetMainModulePlaceHolderListRequest
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
                var problem = new CustomValidationProblemDetails(nameof(GetMainModulePlaceHolderListAsync), e, _logger);
                return BadRequest(problem); 
            }                
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<MainModulePlaceHolderModel>> GetMainModulePlaceHolderItemAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(GetMainModulePlaceHolderItemAsync), id);
            try
            {
                var request = new GetMainModulePlaceHolderItemRequest
                {
                    Id = id
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(GetMainModulePlaceHolderItemAsync), e, _logger);
                return BadRequest(problem);
            }
        }

        [HttpPut]
        public async Task<ActionResult<MainModulePlaceHolderModel>> UpdateMainModulePlaceHolderAsync(MainModulePlaceHolderModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: MainModulePlaceHolder={MainModulePlaceHolder}", nameof(UpdateMainModulePlaceHolderAsync), template);
            try
            {                
                var request = new UpdateMainModulePlaceHolderRequest
                {
                    MainModulePlaceHolder = template,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(UpdateMainModulePlaceHolderAsync), e, _logger);
                return BadRequest(problem);
            }
        }
      
        [HttpPost]
        public async Task<ActionResult<MainModulePlaceHolderModel>> AddMainModulePlaceHolderAsync(MainModulePlaceHolderModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: MainModulePlaceHolder={MainModulePlaceHolder}", nameof(AddMainModulePlaceHolderAsync), template);
            try
            {
                var request = new AddMainModulePlaceHolderRequest
                {
                    MainModulePlaceHolder = template,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(AddMainModulePlaceHolderAsync), e, _logger);
                return BadRequest(problem);
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainModulePlaceHolderAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(DeleteMainModulePlaceHolderAsync), id);
            try
            {
                var request = new DeleteMainModulePlaceHolderRequest
                {
                    Id = id
                };
                await _mediator.Send(request);
                return NoContent();
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(DeleteMainModulePlaceHolderAsync), e, _logger);
                return BadRequest(problem);
            }
        }   
    }
}
