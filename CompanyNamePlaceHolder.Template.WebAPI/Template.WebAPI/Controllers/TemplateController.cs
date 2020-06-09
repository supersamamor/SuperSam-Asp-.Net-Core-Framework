using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Template.WebAPI.Commands.AddTemplate;
using Template.WebAPI.Commands.DeleteTemplate;
using Template.WebAPI.Commands.UpdateTemplate;
using Template.WebAPI.Models;
using Template.WebAPI.Queries.GetTemplateItem;
using Template.WebAPI.Queries.GetTemplateList;
namespace Template.WebAPI.Controllers
{
    [Route("TemplateAPI/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TemplateController : ControllerBase
    {      
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public TemplateController(IMediator mediator, ILogger<TemplateController> logger)
        {        
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CustomPagedList<TemplateModel>>> GetTemplateListAsync(string searchKey, string orderBy, string sortBy, int pageIndex, int pageSize)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: PageIndex={PageIndex}, PageSize={PageSize}", 
                nameof(GetTemplateListAsync), pageIndex, pageSize);
            try
            {                
                var request = new GetTemplateListRequest
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
                var problem = new CustomValidationProblemDetails(nameof(GetTemplateListAsync), e, _logger);
                return BadRequest(problem); 
            }                
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateModel>> GetTemplateItemAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(GetTemplateItemAsync), id);
            try
            {
                var request = new GetTemplateItemRequest
                {
                    Id = id
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(GetTemplateItemAsync), e, _logger);
                return BadRequest(problem);
            }
        }

        [HttpPut]
        public async Task<ActionResult<TemplateModel>> UpdateTemplateAsync(TemplateModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: Template={Template}", nameof(UpdateTemplateAsync), template);
            try
            {                
                var request = new UpdateTemplateRequest
                {
                    Template = template,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(UpdateTemplateAsync), e, _logger);
                return BadRequest(problem);
            }
        }
      
        [HttpPost]
        public async Task<ActionResult<TemplateModel>> AddTemplateAsync(TemplateModel template)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: Template={Template}", nameof(AddTemplateAsync), template);
            try
            {
                var request = new AddTemplateRequest
                {
                    Template = template,
                    Username = Request.Headers["UserName"].ToString()
                };
                return await _mediator.Send(request);
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(AddTemplateAsync), e, _logger);
                return BadRequest(problem);
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplateAsync(int id)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: id={Id}", nameof(DeleteTemplateAsync), id);
            try
            {
                var request = new DeleteTemplateRequest
                {
                    Id = id
                };
                await _mediator.Send(request);
                return NoContent();
            }
            catch (Exception e)
            {
                var problem = new CustomValidationProblemDetails(nameof(DeleteTemplateAsync), e, _logger);
                return BadRequest(problem);
            }
        }   
    }
}
