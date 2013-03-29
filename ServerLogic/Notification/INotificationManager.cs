namespace ServerLogic.Notification
{
    using System;

    public interface INotificationManager
    {
        void CreateNewNotifications();

        void SendAllActiveNotifications();

        void CloseNatificationById(Guid notificationId);
    }
}
