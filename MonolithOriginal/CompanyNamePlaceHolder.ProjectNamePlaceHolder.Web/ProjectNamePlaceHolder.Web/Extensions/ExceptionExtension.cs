using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
namespace ProjectNamePlaceHolder.Web.Extensions
{
    public static class ExceptionExtension
    {
        public static ProblemDetails GenerateProblemDetailsOnHandledExceptions(this Exception exception)
        {
            if (exception is DbUpdateException)
            {
                SqlException sqlException = (SqlException)exception.InnerException;
                if (sqlException != null && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    Regex regex = new Regex(@"[^.]* The duplicate key value is [^.]*\.");
                    Match match = regex.Match(sqlException.Message);
                    if (match.Success)
                    {
                        var message = string.Format("The record already exists.  {0}", match.Value);
                        return new ProblemDetails { Title = "Error occured", Detail = message };
                    }
                }
                else if (sqlException != null && (sqlException.Number == 515 || sqlException.Number == 547))
                {
                    return new ProblemDetails { Title = "Error occured", Detail = "There are missing required fields.  Please check the file to ensure all required fields are filled up.." };
                }
            }
            else if (exception is ValidationException)
            {
                if (exception.Message != null)
                {
                    return new ProblemDetails { Title = "Error occured", Detail = exception.Message };
                }
            }
            return null;
        }
    }
}
