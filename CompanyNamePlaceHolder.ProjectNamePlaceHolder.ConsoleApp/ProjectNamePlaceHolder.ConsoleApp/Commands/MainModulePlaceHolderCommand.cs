using Serilog;
using System;
namespace ProjectNamePlaceHolder.ConsoleApp.Commands
{
    public class MainModulePlaceHolderCommand : BaseCommand
    {
        private string _sampleParameter;
        public MainModulePlaceHolderCommand()
        {
            IsCommand("MainModulePlaceHolder", "");
            HasOption("SampleParameter|s=", "Sample Parameter", d => _sampleParameter = d);
        }

        public override int Run(string[] remainingArguments)
        {
            try
            {
                Log.Logger.Information("CommandName : MainModulePlaceHolder , Arguments : {Arguments}", remainingArguments);
                var mainModulePlaceHolderList = _processService.GetMainModulePlaceHolderList();
                foreach (var item in mainModulePlaceHolderList)
                {
                    Console.WriteLine(@"Item : " + item.Code);
                }
            }
            catch(Exception ex){
                Log.Logger.Error(ex, "CommandName : MainModulePlaceHolder , Arguments : {Arguments}", remainingArguments);
                Console.WriteLine(@"ERROR :" + ex.Message);
            }
            Log.CloseAndFlush();
            return 0;
        }     
    }
}
