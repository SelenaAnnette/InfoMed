namespace ServerLogic.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using DataLayer.Persistence.Measuring;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;

    using Domain.Measuring;
    using Domain.Medicament;    
    using Domain.Message;

    using ServerLogic.Logger;

    public class NotificationManager : INotificationManager
    {
        private readonly IAssignedMedicamentRepository assignedMedicamentRepository;

        private readonly IAssignedMeasuringRepository assignedMeasuringRepository;

        private readonly IMedicamentFormRepository medicamentFormRepository;

        private readonly INotificationRepository notificationRepository;

        private readonly IMeasuringNotificationRepository measuringNotificationRepository;

        private readonly NotificationFactory notificationFactory;

        private readonly MeasuringNotificationFactory measuringNotificationFactory;

        private readonly int startDayFromHour;

        private readonly int endDayFromHour;

        private readonly int reservHoursForAnsver;

        private readonly int minutesCountForNotificationAnswer;

        private readonly ILogger logger;

        public NotificationManager(IAssignedMedicamentRepository assignedMedicamentRepository, INotificationRepository notificationRepository,
            IMedicamentFormRepository medicamentFormRepository, IAssignedMeasuringRepository assignedMeasuringRepository, IMeasuringNotificationRepository measuringNotificationRepository,
            int startDayFromHour, int endDayFromHour, int reservHoursForAnsver, int minutesCountForNotificationAnswer, ILogger logger)
        {
            this.assignedMedicamentRepository = assignedMedicamentRepository;
            this.assignedMeasuringRepository = assignedMeasuringRepository;
            this.medicamentFormRepository = medicamentFormRepository;
            this.measuringNotificationRepository = measuringNotificationRepository;
            this.logger = logger;
            this.notificationRepository = notificationRepository;                        
            this.notificationFactory = new NotificationFactory();
            this.measuringNotificationFactory = new MeasuringNotificationFactory();
            this.startDayFromHour = startDayFromHour;
            this.endDayFromHour = endDayFromHour;
            this.reservHoursForAnsver = reservHoursForAnsver;
            this.minutesCountForNotificationAnswer = minutesCountForNotificationAnswer;
        }

        public void CreateNewNotifications()
        {            
            this.CreateAssignedMedicamentNotifications();
            this.CreateAssignedMeasuringNotifications();
        }

        public IEnumerable<Notification> GetNotificationsForSending()
        {
            return this.notificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now);
        }

        public IEnumerable<MeasuringNotification> GetMeasuringNotificationsForSending()
        {
            return this.measuringNotificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now);
        }

        public IEnumerable<Notification> GetNotificationsForPerson(Guid personId)
        {
            return this.notificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now && v.PersonId == personId);
        }

        public IEnumerable<MeasuringNotification> GetMeasuringNotificationsForPerson(Guid personId)
        {
            return this.measuringNotificationRepository.GetEntitiesByQuery(v => v.IsActive && v.SendingDate <= DateTime.Now && v.PersonId == personId);
        }

        public void CloseNonAnsweredNotifications()
        {
            this.CloseNonAnsweredMedicamentNotifications();
            this.CloseNonAnsweredMeasuringNotifications();
        }

        public void CloseNotificationById(Guid notificationId)
        {
            var notification = this.notificationRepository.GetEntityById(notificationId);
            if (notification != null)
            {
                notification.IsActive = false;
                notification.ExecutedDate = DateTime.Now;
                this.notificationRepository.CreateOrUpdateEntity(notification);
                this.logger.LogMessage(string.Format("Notification {0} was closed", notification.Id));    
            }
            else
            {
                var measuringNotification = this.measuringNotificationRepository.GetEntityById(notificationId);
                measuringNotification.IsActive = false;
                measuringNotification.ExecutedDate = DateTime.Now;
                this.measuringNotificationRepository.CreateOrUpdateEntity(measuringNotification);
                this.logger.LogMessage(string.Format("MeasuringNotification {0} was closed", measuringNotification.Id));
            }
            
        }

        private void CloseNonAnsweredMeasuringNotifications()
        {
            var nonAnsweredNotifications = this.measuringNotificationRepository.GetEntitiesByQuery(
                v => v.IsActive && (DateTime.Now - v.SendingDate).TotalMinutes >= this.minutesCountForNotificationAnswer);
            nonAnsweredNotifications.AsParallel().ForAll(
                nonAnsweredNotification =>
                {
                    nonAnsweredNotification.IsActive = false;
                    this.measuringNotificationRepository.CreateOrUpdateEntity(nonAnsweredNotification);
                    this.logger.LogMessage(string.Format("MeasuringNotification {0} was closed as non-answered", nonAnsweredNotification.Id));
                });
        }

        private void CloseNonAnsweredMedicamentNotifications()
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

        private void CreateAssignedMedicamentNotifications()
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

        private void CreateAssignedMeasuringNotifications()
        {
            var assignedMeasurings = this.assignedMeasuringRepository.GetEntitiesByQuery(v => v.StartDate.Date <= DateTime.Now.Date && v.FinishDate.Date >= DateTime.Now.Date);
            assignedMeasurings.AsParallel().ForAll(assignedMeasuring =>
            {
                var measuringNotification = this.measuringNotificationRepository.GetEntitiesByQuery(
                    v =>
                    v.IsActive && v.AssignedMeasuringId == assignedMeasuring.Id).FirstOrDefault();

                if (measuringNotification == null)
                {
                    this.CreateNewNotification(assignedMeasuring);
                }
            }); 
        }

        private void CreateNewNotification(AssignedMeasuring assignedMeasuring)
        {
            var periodInMinutes = this.GetPeriodInMinutes(assignedMeasuring.Frequency);
            if (this.IsDailyNotificationCountEnough(assignedMeasuring))
            {
                return;
            }

            DateTime sendingDate;
            var measuringNotification = this.GetLastAnsweredNotification(assignedMeasuring);

            if (measuringNotification == null)
            {
                var nonAnsweredNotification = this.GetLastNonAnsweredNotification(assignedMeasuring);
                if (nonAnsweredNotification == null)
                {
                    sendingDate = new DateTime(
                        assignedMeasuring.StartDate.Year, assignedMeasuring.StartDate.Month, assignedMeasuring.StartDate.Day, this.startDayFromHour, 0, 0);
                }
                else
                {
                    sendingDate = this.GetSendingDate(nonAnsweredNotification.SendingDate, periodInMinutes);
                }
            }
            else
            {
                sendingDate = this.GetSendingDate(measuringNotification.ExecutedDate.Value, periodInMinutes);
            }

            if (sendingDate.Date >= assignedMeasuring.FinishDate.Date)
            {
                return;
            }

            var newNotification = this.measuringNotificationFactory.Create(
                        Guid.NewGuid(),
                        assignedMeasuring.Id,
                        assignedMeasuring.PersonConsultation.PatientId,
                        assignedMeasuring.MeasuringTypeId,
                        sendingDate,
                        this.GetNotificationMessage(assignedMeasuring));
            this.measuringNotificationRepository.CreateOrUpdateEntity(newNotification);
            this.logger.LogMessage(string.Format("MeasuringNotification {0} was created", newNotification.Id));
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

        private MeasuringNotification GetLastAnsweredNotification(AssignedMeasuring assignedMeasuring)
        {
            return this.measuringNotificationRepository.GetEntitiesByQuery(
                v => !v.IsActive && v.AssignedMeasuringId == assignedMeasuring.Id && v.ExecutedDate.HasValue)
                       .OrderByDescending(v => v.ExecutedDate)
                       .FirstOrDefault();
        }

        private Notification GetLastAnsweredNotification(AssignedMedicament assignedMedicament)
        {
            return this.notificationRepository.GetEntitiesByQuery(
                v => !v.IsActive && v.AssignedMedicamentId == assignedMedicament.Id && v.ExecutedDate.HasValue)
                       .OrderByDescending(v => v.ExecutedDate)
                       .FirstOrDefault();
        }

        private MeasuringNotification GetLastNonAnsweredNotification(AssignedMeasuring assignedMeasuring)
        {
            return this.measuringNotificationRepository.GetEntitiesByQuery(
                v =>
                !v.IsActive && v.AssignedMeasuringId == assignedMeasuring.Id && !v.ExecutedDate.HasValue).OrderByDescending(v => v.SendingDate).FirstOrDefault();
        }

        private Notification GetLastNonAnsweredNotification(AssignedMedicament assignedMedicament)
        {
            return this.notificationRepository.GetEntitiesByQuery(
                v =>
                !v.IsActive && v.AssignedMedicamentId == assignedMedicament.Id && !v.ExecutedDate.HasValue).OrderByDescending(v => v.SendingDate).FirstOrDefault();
        }

        private bool IsDailyNotificationCountEnough(AssignedMeasuring assignedMeasuring)
        {
            if (assignedMeasuring.Frequency >= 1)
            {
                return false;
            }

            var maxDailyNotificationCount = (int)Math.Round(1 / assignedMeasuring.Frequency);
            var dailyNotificationCount = this.measuringNotificationRepository.GetEntitiesByQuery(
                v =>
                v.AssignedMeasuringId == assignedMeasuring.Id
                && v.SendingDate.Date == DateTime.Now.Date).Count();

            return maxDailyNotificationCount == dailyNotificationCount;
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

        private string GetNotificationMessage(AssignedMeasuring assignedMeasuring)
        {
            return string.Format(
                "Измерьте {0} в единицах {1} с кодом {2}",
                assignedMeasuring.MeasuringType.Title,
                assignedMeasuring.MeasuringType.Measuring,
                assignedMeasuring.MeasuringType.Code);
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
