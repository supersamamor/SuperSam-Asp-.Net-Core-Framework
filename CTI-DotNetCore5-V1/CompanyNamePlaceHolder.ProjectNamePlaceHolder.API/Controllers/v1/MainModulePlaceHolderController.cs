using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.AreaPlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MainModulePlaceHolderController : BaseApiController<MainModulePlaceHolderController>
    {
        [Authorize(Policy = Permission.Projects.View)]
        [ProducesResponseType(typeof(PagedListResponse<MainModulePlaceHolder>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetMainModulePlaceHolderQuery query) =>
            Ok(await Mediator.Send(query));

        [Authorize(Policy = Permission.Projects.View)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id) =>
            await Mediator.Send(new GetMainModulePlaceHolderByIdQuery(id)).ToActionResult();

        [Authorize(Policy = Permission.Projects.Create)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProjectViewModel request) =>
            await Mediator.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.Projects.Edit)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ProjectViewModel request) =>
            await Mediator.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.Projects.Delete)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) =>
            await Mediator.Send(new DeleteMainModulePlaceHolderCommand(id)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });
    }
    public record ProjectViewModel
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
