using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNamePlaceHolder.Services.Infrastructure.Configurations
{
    public class ServiceAPIOptions
    {
        public int RequestTimeout { get; set; } = 180;
        public bool DisableServerCertificateValidation { get; set; } = false;

        // Extend if necessary
        // Add Refence ServiceAPI BaseUrl/s here
    }
}
