using Microsoft.AspNetCore.Mvc.RazorPages;
using Template.Web.AppException;
namespace Template.Web.Extensions
{
    public static class PageModelExtension
    {
        public static void ValidateModelState(this PageModel pageModel) {
            if (!pageModel.ModelState.IsValid)
            {
                string modelStateError = "";
                foreach (var modelState in pageModel.ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        modelStateError = error.ErrorMessage.ToString();
                    }
                }
                throw new ModelStateException(modelStateError);
            }           
        }
    }
}
