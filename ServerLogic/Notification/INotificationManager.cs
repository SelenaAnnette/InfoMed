namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;

    using Domain.Message;

    public interface INotificationManager
    {
        void CreateNewNotifications();

        IEnumerable<Notification> GetNotificationsForSending();

        IEnumerable<Notification> GetNotificationsForPerson(Guid personId);

        void CloseNotificationById(Guid notificationId);

        void CloseNonAnsweredNotifications();
    }
}
