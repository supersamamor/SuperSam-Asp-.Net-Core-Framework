using Microsoft.Extensions.Logging;
using System;
using ProjectNamePlaceHolder.Web.AppException;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class LoggerExtension
    {
        public static string CustomErrorLogger(this ILogger logger, Exception ex, string methodName, object parameter = null)
        {
            logger.LogError(ex, Resource.MessagePatternErrorLog, methodName, parameter);          
            if (ex is ApiResponseException)
            {
                return ((ApiResponseException)ex).Error.Detail;
            }
            else if (ex is ModelStateException)
            {
                return ex.Message.ToString();
            }
            else
            {
                return Resource.PromptMessageDefaultError;
            }       
        }
    }
}
