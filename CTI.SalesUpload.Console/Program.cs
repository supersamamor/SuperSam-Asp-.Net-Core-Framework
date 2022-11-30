using CTI.SalesUpload.Console.Services;
using NLog;
using System.Configuration;
namespace CTI.SalesUpload.Console
{
    internal class Program
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var authService = new AuthenticationService();
        }
    }
}
