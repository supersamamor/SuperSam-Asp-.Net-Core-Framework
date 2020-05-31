using System;

namespace Template.Web.AppException
{
    public class ModelStateException : Exception 
    {
        public ModelStateException(string message) : base(message)
        { 
        }
    }
}
