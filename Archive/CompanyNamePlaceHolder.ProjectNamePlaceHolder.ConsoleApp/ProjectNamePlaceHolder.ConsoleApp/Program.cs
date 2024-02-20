using ManyConsole;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {

            var commands = GetCommands();
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, System.Console.Out);
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }   
}
