using ManyConsole;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using ProjectNamePlaceHolder.ConsoleApp.Services;

namespace ProjectNamePlaceHolder.ConsoleApp.Commands
{
    public class BaseCommand : ConsoleCommand
    {         
        protected MyServiceConfig _serviceConfig { get; set; }
        protected ProcessService _processService { get; set; }
        public BaseCommand() 
        {         
            var config = ConfigurationFactory.CreateDefaultConfiguration();
            _serviceConfig = config.GetSection("MyServiceConfig").Get<MyServiceConfig>();
            Log.Logger = new LoggerConfiguration().WriteTo.Seq(serverUrl: _serviceConfig.SeqUrl, apiKey: _serviceConfig.SeqApiKey, restrictedToMinimumLevel: LogEventLevel.Verbose)
                           .CreateLogger();
            _processService = new ProcessService(_serviceConfig);
            IsCommand("BaseCommand", "");         
        }
        
        public override int Run(string[] remainingArguments)
        {          
            return 0;
        }      
    }
}
