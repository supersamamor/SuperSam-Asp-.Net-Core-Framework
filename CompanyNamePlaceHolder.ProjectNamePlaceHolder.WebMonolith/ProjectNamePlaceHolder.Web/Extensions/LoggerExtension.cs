using Correlate;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using ProjectNamePlaceHolder.Web.AppException;

namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class LoggerExtension
    {
        public static string CustomErrorLogger(this ILogger logger, Exception ex, ICorrelationContextAccessor _correlationContext, string methodName, object parameter = null)
        {
            logger.LogError(ex, Resource.MessagePatternErrorLog, methodName, parameter);
            string traceId = " / " + "Trace Id: " + _correlationContext.CorrelationContext.CorrelationId;
            if (ex is DbUpdateException)
            {
                SqlException sqlException = (SqlException)ex.InnerException;
                if (sqlException != null && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    Regex regex = new Regex(@"[^.]* The duplicate key value is [^.]*\.");
                    Match match = regex.Match(sqlException.Message);
                    if (match.Success)
                    {
                        return string.Format("The record already exists.  {0}", match.Value) + traceId; ;
                    }
                    else
                    {
                        return "A database error occured" + traceId;
                    }
                }
                else if (sqlException != null && (sqlException.Number == 515 || sqlException.Number == 547))
                {
                    return " There are missing required fields.  Please check the file to ensure all required fields are filled up.." + traceId;
                }
            }
            else if (ex is ValidationException)
            {
                return ex.Message != null ? ex.Message : "Error occured" + traceId;
            }
            else if (ex is ModelStateException)
            {
                return ex.Message.ToString() + traceId;
            }        
            return Resource.PromptMessageDefaultError + traceId;
        }
    }
}
