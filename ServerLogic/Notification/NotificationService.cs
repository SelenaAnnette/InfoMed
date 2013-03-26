namespace ServerLogic.Notification
{
    using System;
    using System.Threading;

    public class NotificationService : INotificationService
    {
        private readonly INotificationManager NotificationManager;

        private Timer creationTimer;

        private Timer sendingTimer;

        private readonly int NotificationCreationFrequencyInMinutes;

        private readonly int NotificationSendingFrequencyInMinutes;

        private readonly int DelayStartForNotificationCreatorInSeconds;

        private readonly int DelayStartForNotificationSenderInSeconds;

        public NotificationService(INotificationManager notificationManager, int notificationCreationFrequencyInMinutes, int notificationSendingFrequencyInMinutes,
            int delayStartForNotificationCreatorInSeconds, int delayStartForNotificationSenderInSeconds)
        {            
            this.NotificationManager = notificationManager;
            this.NotificationCreationFrequencyInMinutes = notificationCreationFrequencyInMinutes;
            this.NotificationSendingFrequencyInMinutes = notificationSendingFrequencyInMinutes;
            this.DelayStartForNotificationCreatorInSeconds = delayStartForNotificationCreatorInSeconds;
            this.DelayStartForNotificationSenderInSeconds = delayStartForNotificationSenderInSeconds;
        }

        ~NotificationService()
        {
            this.StopService();
        }

        public void StartService()
        {
            this.creationTimer = new Timer(this.CreateNotifications, this, TimeSpan.FromSeconds(this.DelayStartForNotificationCreatorInSeconds), 
                TimeSpan.FromMinutes(this.NotificationCreationFrequencyInMinutes));
            this.sendingTimer = new Timer(this.SendNotifications, this, TimeSpan.FromSeconds(this.DelayStartForNotificationSenderInSeconds), 
                TimeSpan.FromMinutes(this.NotificationSendingFrequencyInMinutes));
        }        

        public void StopService()
        {
            this.creationTimer.Dispose();
            this.sendingTimer.Dispose();
        }

        private void CreateNotifications(object state)
        {
            this.NotificationManager.CreateNewNotifications();
        }

        private void SendNotifications(object state)
        {
            this.NotificationManager.SendAllActiveNotifications();
        }
    }
}
