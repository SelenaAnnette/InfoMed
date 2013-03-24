namespace ServerLogic.Notification
{
    using System;
    using System.Threading;

    public class NotificationService : INotificationService
    {
        private readonly INotificationManager NotificationManager;

        private Timer timer;

        private readonly int NotificationCreationFrequencyInMinutes;

        public NotificationService(INotificationManager notificationManager, int notificationCreationFrequencyInMinutes)
        {            
            this.NotificationManager = notificationManager;
            this.NotificationCreationFrequencyInMinutes = notificationCreationFrequencyInMinutes;
        }

        ~NotificationService()
        {
            this.StopService();
        }

        public void StartService()
        {
            this.timer = new Timer(this.CreateNotifications, this, TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(this.NotificationCreationFrequencyInMinutes));
        }        

        private void StopService()
        {
            this.timer.Dispose();
        }

        private void CreateNotifications(object state)
        {
            this.NotificationManager.CreateNewNotifications();
        }
    }
}
