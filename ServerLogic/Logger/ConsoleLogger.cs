namespace ServerLogic.Logger
{
    using System;

    public class ConsoleLogger : ILogger
    {        
        public void LogMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);            
        }

        public void LogError(string error)
        {            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}
