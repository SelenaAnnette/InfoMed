namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;

    using Domain.Message;

    public interface INotificationManager
    {
        IEnumerable<Notification> GetAllActiveNotifications();        

        IEnumerable<Notification> GetActiveNotificationsByPersonId(Guid personId);

        void CreateNewNotifications();

        void CloseNatificationById(Guid notificationId);
    }
}
