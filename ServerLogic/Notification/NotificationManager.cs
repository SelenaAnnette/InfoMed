namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;    

    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;    

    using Domain.Medicament;    
    using Domain.Message;

    using ServerLogic.Logger;

    public class NotificationManager : INotificationManager
    {
        private readonly IAssignedMedicamentRepository assignedMedicamentRepository;

        private readonly IMedicamentFormRepository medicamentFormRepository;

        private readonly INotificationRepository notificationRepository;

        private readonly NotificationFactory notificationFactory;

        private readonly int startDayFromHour;

        private readonly int endDayFromHour;

        private readonly int reservHoursForAnsver;

        private readonly int minutesCountForNotificationAnswer;

        private readonly ILogger logger;

        public NotificationManager(IAssignedMedicamentRepository assignedMedicamentRepository, INotificationRepository notificationRepository,
            IMedicamentFormRepository medicamentFormRepository,
            int startDayFromHour, int endDayFromHour, int reservHoursForAnsver, int minutesCountForNotificationAnswer, ILogger logger)
        {
            this.assignedMedicamentRepository = assignedMedicamentRepository;
            this.medicamentFormRepository = medicamentFormRepository;
            this.logger = logger;
            this.notificationRepository = notificationRepository;                        
            this.notificationFactory = new NotificationFactory();
            this.startDayFromHour = startDayFromHour;
            this.endDayFromHour = endDayFromHour;
            this.reservHoursForAnsver = reservHoursForAnsver;
            this.minutesCountForNotificationAnswer = minutesCountForNotificationAnswer;
        }

        public void CreateNewNotifications()
        {            
            var assignedMedicaments = this.assignedMedicamentRepository.GetEntitiesByQuery(v => v.StartDate.Date <= DateTime.Now.Date && v.FinishDate.Date >= DateTime.Now.Date);
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

        public IEnumerable<Notification> GetNotificationsForPerson(Guid personId)
        {
            return this.notificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now && v.PersonId == personId);
        }

        public void CloseNonAnsweredNotifications()
        {
            var nonAnsweredNotifications = this.notificationRepository.GetEntitiesByQuery(
                v => v.IsActive && (DateTime.Now - v.SendingDate).TotalMinutes >= this.minutesCountForNotificationAnswer);
            nonAnsweredNotifications.AsParallel().ForAll(
                nonAnsweredNotification =>
                    {
                        nonAnsweredNotification.IsActive = false;
                        this.notificationRepository.CreateOrUpdateEntity(nonAnsweredNotification);
                        this.logger.LogMessage(string.Format("Notification {0} was closed as non-answered", nonAnsweredNotification.Id));
                    });
        }

        public void CloseNotificationById(Guid notificationId)
        {
            var notification = this.notificationRepository.GetEntityById(notificationId);
            notification.IsActive = false;
            notification.ExecutedDate = DateTime.Now;
            this.notificationRepository.CreateOrUpdateEntity(notification);
            this.logger.LogMessage(string.Format("Notification {0} was closed", notification.Id));
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
                        assignedMedicament.PersonConsultation.PatientId,
                        assignedMedicament.MedicamentId,
                        sendingDate,
                        this.GetNotificationMessage(assignedMedicament));
            this.notificationRepository.CreateOrUpdateEntity(newNotification);
            this.logger.LogMessage(string.Format("Notification {0} was created", newNotification.Id));
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
            if (sendingDate < DateTime.Now)
            {
                sendingDate = DateTime.Now;
            }

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
            var medicamentForm = this.medicamentFormRepository.GetEntityById(assignedMedicament.Medicament.Id);
            return string.Format(
                "Примите {0} {1} {2} с кодом {3}",
                assignedMedicament.Medicament.Name,
                assignedMedicament.Dosage,
                medicamentForm.Name,
                assignedMedicament.Medicament.Code);
        }
    }
}
