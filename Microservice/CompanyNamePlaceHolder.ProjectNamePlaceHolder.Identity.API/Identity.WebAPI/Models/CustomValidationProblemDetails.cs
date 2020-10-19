using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Identity.WebAPI.Models
{
    public class CustomValidationProblemDetails : ProblemDetails
    {
        public CustomValidationProblemDetails(string methodName, Exception ex, ILogger logger)
        {
            logger.LogError(ex, "Error in {MethodName}", methodName);
            this.Title = "Error occured";
            if (ex is DbUpdateException)
            {
                SqlException sqlException = (SqlException)ex.InnerException;
                if (sqlException != null && (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    Regex regex = new Regex(@"[^.]* The duplicate key value is [^.]*\.");
                    Match match = regex.Match(sqlException.Message);
                    if (match.Success)
                    {
                        this.Detail = string.Format("The record already exists.  {0}", match.Value);
                    }
                    else
                    {
                        this.Detail = "A database error occured";
                    }
                }
                else if (sqlException != null && (sqlException.Number == 515 || sqlException.Number == 547))
                {
                    this.Detail = " There are missing required fields.  Please check the file to ensure all required fields are filled up..";
                }
            }
            else if (ex is ValidationException)
            {
                this.Detail = ex.Message != null ? ex.Message : "Error occured";
            }
            else
            {
                this.Detail = "An error has occured.";
            }
        }
    }
}
