namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;

    using Domain.Message;
    using Domain.RiskFactor;

    public interface INotificationManager
    {
        void CreateNewNotifications();

        void CreateOnceRiskFactorNotification(Guid personId, IEnumerable<RiskFactor> riskFactors);

        IEnumerable<Notification> GetNotificationsForSending();

        IEnumerable<MeasuringNotification> GetMeasuringNotificationsForSending();

        IEnumerable<OnceRiskFactorNotification> GetRiskFactorsNotificationsForSending();

        IEnumerable<Notification> GetNotificationsForPerson(Guid personId);

        IEnumerable<MeasuringNotification> GetMeasuringNotificationsForPerson(Guid personId);

        void CloseNotificationById(Guid notificationId);

        void CloseNonAnsweredNotifications();
    }
}
