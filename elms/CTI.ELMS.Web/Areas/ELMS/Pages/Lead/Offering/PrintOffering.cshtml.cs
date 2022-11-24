using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using CTI.ELMS.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead.Offering;

[Authorize(Policy = Permission.Offering.Print)]
public class PrintOfferingModel : BasePageModel<PrintOfferingModel>
{
    private readonly string _staticFolderPath;
    public PrintOfferingModel(IConfiguration configuration)
    {
        _staticFolderPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
    }
    public RotativaDocumentModel Document { get; set; } = new();
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string id)
    {
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByOfferingIdQuery(id, Constants.TabNavigation.Offerings)));
        OfferingViewModel offering = new();
        _ = (await Mediatr.Send(new GetOfferingByIdQuery(id))).Select(l => offering = Mapper.Map<OfferingViewModel>(l));
        var rotativaService = new RotativaService<OfferingViewModel>(offering, "Pdf\\OfferSheet", $"{offering.OfferSheetNo}.pdf",
                                                            WebConstants.UploadFilesPath, _staticFolderPath, "OfferSheet");
        Document = await rotativaService.GeneratePDFAsync(PageContext);
        return Page();
    }
}
