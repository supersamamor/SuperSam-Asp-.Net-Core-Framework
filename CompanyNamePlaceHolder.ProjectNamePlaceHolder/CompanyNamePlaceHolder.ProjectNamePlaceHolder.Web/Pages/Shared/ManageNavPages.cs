using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Pages.Shared;

public static class ManageNavPages
{
    public static string? PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActiveMainPage"] as string
            ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }

    public static string? Level1Class(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["Level1"] as string
            ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "menu-open" : null;
    }
}
