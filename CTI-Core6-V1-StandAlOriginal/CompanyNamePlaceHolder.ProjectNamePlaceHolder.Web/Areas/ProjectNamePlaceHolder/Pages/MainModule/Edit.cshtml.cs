using CompanyNamePlaceHolder.Common.Web.Utility.Helpers;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.MainModule;

[Authorize(Policy = Permission.MainModule.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    private readonly IConfiguration _configuration;
    public EditModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [BindProperty]
    public MainModuleViewModel MainModule { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetMainModuleByIdQuery(id)), MainModule);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        await UploadFile("MainModule", "FieldName", MainModule.Id, MainModule.FileUpload);
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditMainModuleCommand>(MainModule)), "Details", true);
    }
    public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "AddSubDetailList")
        {
            return AddSubDetailList();
        }
        if (AsyncAction == "RemoveSubDetailList")
        {
            return RemoveSubDetailList();
        }


        return Partial("_InputFieldsPartial", MainModule);
    }

    private IActionResult AddSubDetailList()
    {
        ModelState.Clear();
        if (MainModule!.SubDetailListList == null) { MainModule!.SubDetailListList = new List<SubDetailListViewModel>(); }
        MainModule!.SubDetailListList!.Add(new SubDetailListViewModel() { TestForeignKeyOne = MainModule.Id });
        return Partial("_InputFieldsPartial", MainModule);
    }
    private IActionResult RemoveSubDetailList()
    {
        ModelState.Clear();
        MainModule.SubDetailListList = MainModule!.SubDetailListList!.Where(l => l.Id != RemoveSubDetailId).ToList();
        return Partial("_InputFieldsPartial", MainModule);
    }
    public async Task<string> UploadFile(string moduleName, string fieldName, string id, IFormFile? formFile)
    {
        string filePath = "";
        if (formFile != null)
        {
            var permittedExtensions = _configuration.GetValue<string>("UsersUpload:DocumentPermitedExtensions").Split(',').ToArray();
            var fileSizeLimit = _configuration.GetValue<long>("UsersUpload:FileSizeLimit");
            var _targetFilePath = _configuration.GetValue<string>("UsersUpload:UploadFilesPath") + "\\" + moduleName + "\\" + id + "\\" + fieldName;
            bool exists = System.IO.Directory.Exists(_targetFilePath);
            if (!exists)
                System.IO.Directory.CreateDirectory(_targetFilePath);
            _ = (await FileHelper.ProcessFormFile<MainModuleViewModel, string>(formFile!,
                                             permittedExtensions,
                                             fileSizeLimit,
                                             cancellationToken: new CancellationToken(),
                                             f: s =>
                                             {
                                                 var trustedFileNameForFileStorage = Path.GetRandomFileName();
                                                 var filePath = Path.Combine(_targetFilePath, trustedFileNameForFileStorage);
                                                 using (var file = System.IO.File.Create(filePath))
                                                 {
                                                     s.WriteTo(file);
                                                 }                                                   
                                                 return filePath;
                                             })).Select(l => filePath = l);
        }
        return filePath;
    }
}
