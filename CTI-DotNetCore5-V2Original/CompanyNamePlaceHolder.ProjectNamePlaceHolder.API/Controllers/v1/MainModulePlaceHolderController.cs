using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenIddict.Validation.AspNetCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MainModulePlaceHolderController : BaseApiController<MainModulePlaceHolderController>
    {
        [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
        [ProducesResponseType(typeof(PagedListResponse<MainModulePlaceHolder>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetMainModulePlaceHolderQuery query) =>
            Ok(await Mediator.Send(query));

        [Authorize(Policy = Permission.MainModulePlaceHolder.View)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id) =>
            await Mediator.Send(new GetMainModulePlaceHolderByIdQuery(id)).ToActionResult();

        [Authorize(Policy = Permission.MainModulePlaceHolder.Create)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] MainModulePlaceHolderViewModel request) =>
            await Mediator.Send(Mapper.Map<AddMainModulePlaceHolderCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] MainModulePlaceHolderViewModel request) =>
            await Mediator.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(request)).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });

        [Authorize(Policy = Permission.MainModulePlaceHolder.Delete)]
        [ProducesResponseType(typeof(MainModulePlaceHolder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) =>
            await Mediator.Send(new DeleteMainModulePlaceHolderCommand { Id = id }).ToActionResult(
                success: null,
                errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    return BadRequest(ModelState);
                });
    }

    public record MainModulePlaceHolderViewModel
    {
        [Required]
        public string Id { get; set; } = "";
        [Required]
        public string Code { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string EntityCode { get; init; } = "";
        [Required]
        public string IfcaProjectCode { get; init; } = "";
        [Required]
        public string Address { get; init; } = "";
        [Required]
        public string Location { get; init; } = "";
        [Required]
        public string Status { get; init; } = "";
        [Required]
        public string Type { get; init; } = "";
        [Required]
        public string Owner { get; init; } = "";
        [Required]
        public string Description { get; init; } = "";
        [Required]
        public string Division { get; init; } = "";
        [Required]
        public DateTime StartDate { get; init; }
        [Required]
        public DateTime CompletionDate { get; init; }
        [Required]
        public int TrxDays { get; init; }
        [Required]
        public string Department { get; init; } = "";
        [Required]
        public string ContactDetails { get; init; } = "";
        [Required]
        public string Category { get; init; } = "";
        [Required]
        public string ProductUse { get; init; } = "";
        [Required]
        public string MarketSegment { get; init; } = "";
        [Required]
        public string Brand { get; init; } = "";
    }
}
