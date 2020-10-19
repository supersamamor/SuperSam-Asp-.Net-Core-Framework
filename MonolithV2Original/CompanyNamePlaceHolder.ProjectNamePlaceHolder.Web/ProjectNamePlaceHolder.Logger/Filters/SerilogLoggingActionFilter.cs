using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ProjectNamePlaceHolder.Logger.Filters
{
    public class SerilogLoggingActionFilter : IActionFilter
    {
        private readonly IDiagnosticContext DiagnosticContext;

        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
            DiagnosticContext = diagnosticContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            DiagnosticContext.Set("ActionArguments", context.ActionArguments, true);
            DiagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
            DiagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            DiagnosticContext.Set("ValidationState", context.ModelState.IsValid);
        }
    }
}
