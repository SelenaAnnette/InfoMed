namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;

    using Domain.Medicament;    
    using Domain.Message;    

    using ServerLogic.Logger;

    public class NotificationManager : INotificationManager
    {
        private readonly IAssignedMedicamentRepository AssignedMedicamentRepository;

        private readonly INotificationRepository NotificationRepository;

        private readonly IPersonContactRepository PersonContactRepository;        

        private readonly NotificationFactory NotificationFactory;

        private readonly ILogger Logger;

        private readonly int StartDayFromHour;

        private readonly int EndDayFromHour;

        private readonly int ReservHoursForAnsver;

        public NotificationManager(IAssignedMedicamentRepository assignedMedicamentRepository, ILogger logger, 
            INotificationRepository notificationRepository, int startDayFromHour, int endDayFromHour, int reservHoursForAnsver,
            IPersonContactRepository personContactRepository)
        {
            this.AssignedMedicamentRepository = assignedMedicamentRepository;
            this.NotificationRepository = notificationRepository;            
            this.PersonContactRepository = personContactRepository;            
            this.Logger = logger;
            this.NotificationFactory = new NotificationFactory();
            this.StartDayFromHour = startDayFromHour;
            this.EndDayFromHour = endDayFromHour;
            this.ReservHoursForAnsver = reservHoursForAnsver;
        }        

        public void CreateNewNotifications()
        {
            var assignedMedicaments = this.AssignedMedicamentRepository.GetEntitiesByQuery(v => v.IsActual);
            assignedMedicaments.AsParallel().ForAll(assignedMedicament =>
            {
                var notification = this.NotificationRepository.GetEntitiesByQuery(
                    v =>
                    v.IsActive && v.PersonId == assignedMedicament.PersonId
                    && v.MedicamentId == assignedMedicament.MedicamentId).FirstOrDefault();

                if (notification == null)
                {
                    this.CreateNewNotification(assignedMedicament);
                }                
            });            
        }

        public void SendAllActiveNotifications()
        {
            var notifications =
                this.NotificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now);
            notifications.AsParallel().ForAll(this.SendNotification);
        }

        public void CloseNatificationById(Guid notificationId)
        {
            var notification = this.NotificationRepository.GetEntityById(notificationId);
            notification.IsActive = false;
            notification.ExecutedDate = DateTime.Now;
            this.NotificationRepository.CreateOrUpdateEntity(notification);
        }

        private void CreateNewNotification(AssignedMedicament assignedMedicament)
        {
            var periodInMinutes = this.GetPeriodInMinutes(assignedMedicament.Frequency);
            if (this.IsDailyNotificationCountEnough(assignedMedicament))
            {
                return;
            }

            var notification = this.NotificationRepository.GetEntitiesByQuery(
                v =>
                !v.IsActive && v.PersonId == assignedMedicament.PersonId
                && v.MedicamentId == assignedMedicament.MedicamentId).OrderBy(v => v.ExecutedDate).LastOrDefault();
            var sendingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, this.StartDayFromHour, 0, 0);
            if (notification != null)
            {                
                if (notification.ExecutedDate.HasValue)
                {                    
                    sendingDate = this.GetSendingDate(notification.ExecutedDate.Value, periodInMinutes);
                }
                else
                {
                    this.Logger.LogError(string.Format("Notification {0} doesn't have executed date!", notification.Id));
                    return;
                }
            }            
            
            var newNotification = this.NotificationFactory.Create(
                        Guid.NewGuid(),
                        assignedMedicament.PersonId,
                        assignedMedicament.MedicamentId,
                        sendingDate,
                        this.GetNotificationMessage(assignedMedicament));
            this.NotificationRepository.CreateOrUpdateEntity(newNotification);
        }

        private bool IsDailyNotificationCountEnough(AssignedMedicament assignedMedicament)
        {
            if (assignedMedicament.Frequency >= 1)
            {
                return false;
            }

            var maxDailyNotificationCount = (int)(assignedMedicament.Frequency * (this.EndDayFromHour - this.StartDayFromHour));            
            var dailyNotificationCount = this.NotificationRepository.GetEntitiesByQuery(
                v =>
                v.PersonId == assignedMedicament.PersonId && v.MedicamentId == assignedMedicament.MedicamentId
                && v.SendingDate.Date == DateTime.Now.Date).Count();

            return maxDailyNotificationCount == dailyNotificationCount;
        }

        private int GetPeriodInMinutes(double frequency)
        {
            if (frequency < 1)
            {
                return (int)TimeSpan.FromHours(frequency * (this.EndDayFromHour - this.StartDayFromHour)).TotalMinutes;
            }
            
            return (int)TimeSpan.FromDays(frequency * 1).TotalMinutes;            
        }

        private DateTime GetSendingDate(DateTime lastDate, int periodInMinutes)
        {
            var sendingDate = lastDate.AddMinutes(periodInMinutes);
            if (sendingDate.Hour < this.StartDayFromHour)
            {
                return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day, this.StartDayFromHour, 0, 0);
            }

            if (sendingDate.Hour >= this.EndDayFromHour + this.ReservHoursForAnsver)
            {
                return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day + 1, this.StartDayFromHour, 0, 0);
            }
            
            return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day, sendingDate.Hour, sendingDate.Minute, 0);            
        }

        private string GetNotificationMessage(AssignedMedicament assignedMedicament)
        {
            return string.Format(
                "Примите {0} {1} лекарства {2}",
                assignedMedicament.Dosage,
                assignedMedicament.Measure,
                assignedMedicament.Medicament.Name);
        }

        private void SendNotification(Notification notification)
        {
            var personContacts = this.PersonContactRepository.GetEntitiesByQuery(
                v => v.PersonId == notification.PersonId && v.ContactType.Title == "Mobile");            
            //TODO Replace sendAbstract to Send sms method
//            personContacts.AsParallel().ForAll(v => sendAbstract(v.Value));
        }
    }
}
