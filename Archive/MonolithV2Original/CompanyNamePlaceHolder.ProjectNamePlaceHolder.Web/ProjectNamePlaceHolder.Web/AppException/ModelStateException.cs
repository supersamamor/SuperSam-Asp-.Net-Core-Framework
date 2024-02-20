using System;

namespace ProjectNamePlaceHolder.Web.AppException
{
    public class ModelStateException : Exception 
    {
        public ModelStateException(string message) : base(message)
        { 
        }
    }
}
