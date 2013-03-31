namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;    

    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;    

    using Domain.Medicament;    
    using Domain.Message;        

    public class NotificationManager : INotificationManager
    {
        private readonly IAssignedMedicamentRepository assignedMedicamentRepository;

        private readonly INotificationRepository notificationRepository;

        private readonly NotificationFactory notificationFactory;

        private readonly int startDayFromHour;

        private readonly int endDayFromHour;

        private readonly int reservHoursForAnsver;

        private readonly int minutesCountForNotificationAnswer;

        public NotificationManager(IAssignedMedicamentRepository assignedMedicamentRepository, INotificationRepository notificationRepository,
            int startDayFromHour, int endDayFromHour, int reservHoursForAnsver, int minutesCountForNotificationAnswer)
        {
            this.assignedMedicamentRepository = assignedMedicamentRepository;
            this.notificationRepository = notificationRepository;                        
            this.notificationFactory = new NotificationFactory();
            this.startDayFromHour = startDayFromHour;
            this.endDayFromHour = endDayFromHour;
            this.reservHoursForAnsver = reservHoursForAnsver;
            this.minutesCountForNotificationAnswer = minutesCountForNotificationAnswer;
        }

        public void CreateNewNotifications()
        {
            var assignedMedicaments = this.assignedMedicamentRepository.GetEntitiesByQuery(v => v.StartDate.Date >= DateTime.Now.Date && v.FinishDate.Date <= DateTime.Now.Date);
            assignedMedicaments.AsParallel().ForAll(assignedMedicament =>
            {
                var notification = this.notificationRepository.GetEntitiesByQuery(
                    v =>
                    v.IsActive && v.AssignedMedicamentId == assignedMedicament.Id).FirstOrDefault();

                if (notification == null)
                {
                    this.CreateNewNotification(assignedMedicament);
                }                
            });            
        }

        public IEnumerable<Notification> GetNotificationsForSending()
        {
            return this.notificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now);
        }

        public void CloseNonAnsweredNotifications()
        {
            var nonAnweredNotifications = this.notificationRepository.GetEntitiesByQuery(
                v => v.IsActive && (v.SendingDate - DateTime.Now).TotalMinutes < this.minutesCountForNotificationAnswer);
            nonAnweredNotifications.AsParallel().ForAll(
                nonAnweredNotification =>
                    {
                        nonAnweredNotification.IsActive = false;
                        this.notificationRepository.CreateOrUpdateEntity(nonAnweredNotification);
                    });
        }

        public void CloseNotificationById(Guid notificationId)
        {
            var notification = this.notificationRepository.GetEntityById(notificationId);
            notification.IsActive = false;
            notification.ExecutedDate = DateTime.Now;
            this.notificationRepository.CreateOrUpdateEntity(notification);
        }

        private void CreateNewNotification(AssignedMedicament assignedMedicament)
        {
            var periodInMinutes = this.GetPeriodInMinutes(assignedMedicament.Frequency);
            if (this.IsDailyNotificationCountEnough(assignedMedicament))
            {
                return;
            }

            DateTime sendingDate;
            var notification = this.GetLastAnsweredNotification(assignedMedicament);            

            if (notification == null)
            {
                var nonAnsweredNotification = this.GetLastNonAnsweredNotification(assignedMedicament);
                if (nonAnsweredNotification == null)
                {
                    sendingDate = new DateTime(
                        assignedMedicament.StartDate.Year, assignedMedicament.StartDate.Month, assignedMedicament.StartDate.Day, this.startDayFromHour, 0, 0);
                }
                else
                {
                    sendingDate = this.GetSendingDate(nonAnsweredNotification.SendingDate, periodInMinutes);
                }
            }
            else
            {                
                sendingDate = this.GetSendingDate(notification.ExecutedDate.Value, periodInMinutes);                              
            }

            if (sendingDate.Date >= assignedMedicament.FinishDate.Date)
            {
                return;
            } 
            
            var newNotification = this.notificationFactory.Create(
                        Guid.NewGuid(),
                        assignedMedicament.Id,
                        assignedMedicament.PersonId,
                        assignedMedicament.MedicamentId,
                        sendingDate,
                        this.GetNotificationMessage(assignedMedicament));
            this.notificationRepository.CreateOrUpdateEntity(newNotification);
        }

        private Notification GetLastAnsweredNotification(AssignedMedicament assignedMedicament)
        {
            return this.notificationRepository.GetEntitiesByQuery(
                v => !v.IsActive && v.AssignedMedicamentId == assignedMedicament.Id && v.ExecutedDate.HasValue)
                       .OrderByDescending(v => v.ExecutedDate)
                       .FirstOrDefault();
        }

        private Notification GetLastNonAnsweredNotification(AssignedMedicament assignedMedicament)
        {
            return this.notificationRepository.GetEntitiesByQuery(
                v =>
                !v.IsActive && v.AssignedMedicamentId == assignedMedicament.Id && !v.ExecutedDate.HasValue).OrderByDescending(v => v.SendingDate).FirstOrDefault();
        }

        private bool IsDailyNotificationCountEnough(AssignedMedicament assignedMedicament)
        {
            if (assignedMedicament.Frequency >= 1)
            {
                return false;
            }

            var maxDailyNotificationCount = (int)Math.Round(1 / assignedMedicament.Frequency);            
            var dailyNotificationCount = this.notificationRepository.GetEntitiesByQuery(
                v =>
                v.AssignedMedicamentId == assignedMedicament.Id
                && v.SendingDate.Date == DateTime.Now.Date).Count();

            return maxDailyNotificationCount == dailyNotificationCount;
        }

        private int GetPeriodInMinutes(double frequency)
        {
            if (frequency < 1)
            {
                return (int)TimeSpan.FromHours(frequency * (this.endDayFromHour - this.startDayFromHour)).TotalMinutes;
            }
            
            return (int)TimeSpan.FromDays(frequency * 1).TotalMinutes;
        }

        private DateTime GetSendingDate(DateTime lastDate, int periodInMinutes)
        {
            var sendingDate = lastDate.AddMinutes(periodInMinutes);
            if (sendingDate.Hour < this.startDayFromHour)
            {
                return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day, this.startDayFromHour, 0, 0);
            }

            if (sendingDate.Hour >= this.endDayFromHour + this.reservHoursForAnsver)
            {
                return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day + 1, this.startDayFromHour, 0, 0);
            }
            
            return new DateTime(sendingDate.Year, sendingDate.Month, sendingDate.Day, sendingDate.Hour, sendingDate.Minute, 0);            
        }

        private string GetNotificationMessage(AssignedMedicament assignedMedicament)
        {
            return string.Format(
                "Примите {0} {1} лекарства {2} с кодом {3}",
                assignedMedicament.Dosage,
                assignedMedicament.Measure,
                assignedMedicament.Medicament.Name,
                assignedMedicament.Medicament.Code);
        }
    }
}
