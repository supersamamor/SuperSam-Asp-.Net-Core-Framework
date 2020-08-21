using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.AddMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.DeleteMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Commands.MainModulePlaceHolder.UpdateMainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Models;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItem;
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderItemBy[UFF];
using ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderList;
using ProjectNamePlaceHolder.Web.Extensions;
using System;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Web.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MainModulePlaceHolderController : BaseController
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
                _logger.LogError(e, "Error in {MethodName}", nameof(GetMainModulePlaceHolderListAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
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
                _logger.LogError(e, "Error in {MethodName}", nameof(GetMainModulePlaceHolderItemAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<MainModulePlaceHolderModel>> UpdateMainModulePlaceHolderAsync(string userName, MainModulePlaceHolderModel mainModulePlaceHolder)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: MainModulePlaceHolder={MainModulePlaceHolder}", nameof(UpdateMainModulePlaceHolderAsync), mainModulePlaceHolder);
            try
            {
                var request = new UpdateMainModulePlaceHolderRequest
                {
                    MainModulePlaceHolder = mainModulePlaceHolder,
                    Username = userName
                };
                await _mediator.Send(request);

                var updatedMainModulePlaceHolderRequest = new GetMainModulePlaceHolderItemRequest
                {
                    Id = mainModulePlaceHolder.Id
                };
                return await _mediator.Send(updatedMainModulePlaceHolderRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(UpdateMainModulePlaceHolderAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<MainModulePlaceHolderModel>> AddMainModulePlaceHolderAsync(string userName, MainModulePlaceHolderModel mainModulePlaceHolder)
        {
            _logger.LogInformation("MethodName: {MethodName}, Parameters: MainModulePlaceHolder={MainModulePlaceHolder}", nameof(AddMainModulePlaceHolderAsync), mainModulePlaceHolder);
            try
            {
                var request = new AddMainModulePlaceHolderRequest
                {
                    MainModulePlaceHolder = mainModulePlaceHolder,
                    Username = userName
                };
                await _mediator.Send(request);

                var savedMainModulePlaceHolderRequest = new GetMainModulePlaceHolderItemBy[UFF]Request
                {
                    MainModulePlaceHolder[UFF] = mainModulePlaceHolder.[UFF]
                };
                return await _mediator.Send(savedMainModulePlaceHolderRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in {MethodName}", nameof(AddMainModulePlaceHolderAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
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
                _logger.LogError(e, "Error in {MethodName}", nameof(DeleteMainModulePlaceHolderAsync));
                var problem = e.GenerateProblemDetailsOnHandledExceptions();
                if (problem != null)
                {
                    return BadRequest(problem);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }
    }
}
