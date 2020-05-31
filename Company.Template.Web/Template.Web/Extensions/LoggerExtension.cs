using Correlate;
using Microsoft.Extensions.Logging;
using System;
using Template.Web.AppException;

namespace Template.Web.Extensions
{
    public static class LoggerExtension
    {
        public static string CustomErrorLogger(this ILogger logger, Exception ex, ICorrelationContextAccessor _correlationContext, string methodName, object parameter = null)
        {
            logger.LogError(ex, Resource.MessagePatternErrorLog, methodName, parameter);
            string traceId = " / " + "Trace Id: " + _correlationContext.CorrelationContext.CorrelationId;
            if (ex is ApiResponseException)
            {
                return ((ApiResponseException)ex).Error.Detail + traceId;
            }
            else if (ex is ModelStateException)
            {
                return ex.Message.ToString() + traceId;
            }
            else
            {
                return Resource.PromptMessageDefaultError + traceId;
            }       
        }
    }
}
