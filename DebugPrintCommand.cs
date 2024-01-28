using System;

namespace FirstGame
{
    internal class DebugPrintCommand : ICommand
    {

        private string message;

        public DebugPrintCommand(string msg)
        {
            message = msg;
        }

        public void Execute()
        {
            // Print message to console
            Console.WriteLine(message);
        }

    }
}
