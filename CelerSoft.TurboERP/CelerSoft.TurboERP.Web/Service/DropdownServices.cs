using Microsoft.AspNetCore.Mvc.Rendering;
using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.Common.Data;
using CelerSoft.TurboERP.Web.Areas.Admin.Queries.Users;
using MediatR;
using CelerSoft.TurboERP.Web.Areas.Admin.Queries.Roles;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Web.Service
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
		public SelectList GetCustomerList(string id)
		{
			return _context.GetSingle<CustomerState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.TINNumber } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetInventoryList(string id)
		{
			return _context.GetSingle<InventoryState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetOrderList(string id)
		{
			return _context.GetSingle<OrderState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetSupplierList(string id)
		{
			return _context.GetSingle<SupplierState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Company } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetBrandList(string id)
		{
			return _context.GetSingle<BrandState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetItemList(string id)
		{
			return _context.GetSingle<ItemState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Code } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetProductList(string id)
		{
			var product =  _context.Product.Include(l=>l.Brand).Include(l=>l.Item).Where(e => e.Id == id).FirstOrDefault();
			if (product != null)
			{
				return new SelectList(new List<SelectListItem> { new() { Value = product.Id, Text = product.ProductName } }, "Value", "Text", product.Id);
			}
			else
			{
				return new SelectList(new List<SelectListItem>(), "Value", "Text");
			}		
		}
		public SelectList GetSupplierQuotationItemList(string id)
		{
			return _context.GetSingle<SupplierQuotationItemState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetSupplierQuotationList(string id)
		{
			return _context.GetSingle<SupplierQuotationState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetPurchaseRequisitionList(string id)
		{
			return _context.GetSingle<PurchaseRequisitionState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetPurchaseItemList(string id)
		{
			return _context.GetSingle<PurchaseItemState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Id } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetItemTypeList(string id)
		{
			return _context.GetSingle<ItemTypeState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetUnitList(string id)
		{
			return _context.GetSingle<UnitState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Name } }, "Value", "Text", e.Id),
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
