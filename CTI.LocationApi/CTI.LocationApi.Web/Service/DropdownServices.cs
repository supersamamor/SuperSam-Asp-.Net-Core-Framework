using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.LocationApi.Infrastructure.Data;
using CTI.LocationApi.Core.LocationApi;
using CTI.Common.Data;
using CTI.LocationApi.Web.Areas.Admin.Queries.Users;
using MediatR;
using CTI.LocationApi.Web.Areas.Admin.Queries.Roles;

namespace CTI.LocationApi.Web.Service
{
    public class DropdownServices
    {
		private readonly ApplicationContext _context;
		private readonly IMediator _mediaTr;

		public DropdownServices(ApplicationContext context, IMediator mediaTr)
		{            
			_context = context;
			_mediaTr = mediaTr;
		}       
		public SelectList GetProvinceList(string id)
		{
			return _context.GetSingle<ProvinceState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetCountryList(string id)
		{
			return _context.GetSingle<CountryState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetCityList(string id)
		{
			return _context.GetSingle<CityState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetRegionList(string id)
		{
			return _context.GetSingle<RegionState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		
		public async Task<IEnumerable<SelectListItem>> GetUserList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproversQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
public async Task<IEnumerable<SelectListItem>> GetRoleApproverList(string currentSelectedApprover, IList<string> allSelectedApprovers)
		{
			return (await _mediaTr.Send(new GetApproverRolesQuery(currentSelectedApprover, allSelectedApprovers))).Data.Select(l => new SelectListItem { Value = l.Id, Text = l.Name });
		}
    }
}
