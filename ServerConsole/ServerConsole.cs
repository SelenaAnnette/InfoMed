namespace ServerConsole
{
    using System;

    using Ninject;

    using ServerLogic.Logger;
    using ServerLogic.Notification;

    public class ServerConsole
    {
        private static readonly ILogger Logger = Binder.NinjectKernel.Get<ILogger>();

        private static readonly INotificationService NotificationService = Binder.NinjectKernel.Get<INotificationService>();

        static void Main(string[] args)
        {
            Logger.LogMessage("Server start OK");
            StartWork();
            Console.WriteLine("Press q to stop server console");
            var runCicle = true;
            while (runCicle)
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {                    
                    case ConsoleKey.Q:
                        {                            
                            runCicle = false;
                            break;
                        }
                }
            }
        }

        private static void StartWork()
        {
            NotificationService.StartService();
        }
    }
}
