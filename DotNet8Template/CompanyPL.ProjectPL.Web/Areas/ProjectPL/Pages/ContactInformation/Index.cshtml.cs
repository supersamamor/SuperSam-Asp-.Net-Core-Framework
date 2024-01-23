using CompanyPL.ProjectPL.Application.Features.ProjectPL.ContactInformation.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Web.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Services;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.ContactInformation;

[Authorize(Policy = Permission.ContactInformation.View)]
public class IndexModel : BasePageModel<IndexModel>
{   
	private readonly string? _uploadPath;
    public IndexModel(IConfiguration configuration)
    {
        _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
    }
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
	[BindProperty]
    public BatchUploadModel BatchUpload { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetContactInformationQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                EmployeeId = e.Employee?.EmployeeCode,
				e.ContactDetails,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetContactInformationQuery>(nameof(ContactInformationState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, ContactInformationState>(BatchUpload.BatchUploadForm, nameof(ContactInformationState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<ContactInformationState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
