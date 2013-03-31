namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;

    using Domain.Message;

    public interface INotificationManager
    {
        void CreateNewNotifications();

        IEnumerable<Notification> GetNotificationsForSending();

        void CloseNotificationById(Guid notificationId);

        void CloseNonAnsweredNotifications();
    }
}
