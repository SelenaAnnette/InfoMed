namespace ServerConsole
{
    using System;

    using Ninject;

    using ServerLogic.Logger;
    using ServerLogic.Notification;

    public class ServerConsole
    {
        private static ILogger Logger;

        private static INotificationService NotificationService;

        private static bool areServicesRun;        

        private static void InitializeFields()
        {
            Logger = Binder.NinjectKernel.Get<ILogger>();
            NotificationService = Binder.NinjectKernel.Get<INotificationService>();
            areServicesRun = false;
        }

        static void Main(string[] args)
        {            
            InitializeFields();            
            ShowConrolKeys();
            var runCicle = true;
            while (runCicle)
            {
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.S:
                        {
                            if (!areServicesRun)
                            {
                                StartWork();
                            }

                            break;
                        }

                    case ConsoleKey.Q:
                        {
                            if (areServicesRun)
                            {
                                StopWork();
                            }

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            if (areServicesRun)
                            {
                                StopWork();
                            }

                            runCicle = false;
                            break;
                        }                    
                }
            }
        }

        private static void ShowConrolKeys()
        {
            Console.WriteLine("Press ENTER to terminate server console");
            Console.WriteLine("Press Q to stop services");
            Console.WriteLine("Press S to start services");
        }

        private static void StartWork()
        {
            areServicesRun = true;
            NotificationService.StartService();
            Logger.LogMessage("Server started OK");
        }

        private static void StopWork()
        {
            areServicesRun = false;
            NotificationService.StopService();
            Logger.LogMessage("Server stopped OK");
        }
    }
}
