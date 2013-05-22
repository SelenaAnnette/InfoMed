namespace ServerLogic.Notification
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    using DataLayer.Persistence.Person;

    using ServerLogic.Logger;

    using SmsModule;

    public class NotificationService : INotificationService
    {
        private readonly INotificationManager notificationManager;

        private Timer creationTimer;

        private Timer sendingTimer;

        private Timer closingTimer;

        private readonly IPersonContactRepository personContactRepository;  

        private readonly int notificationCreationFrequencyInMinutes;

        private readonly int notificationSendingFrequencyInMinutes;        

        private readonly int delayStartForNotificationTimersInSeconds;

        private readonly bool sendAndReceiveSms;

        private readonly IModem modem;

        private Timer modemTimer;        

        private readonly int periodOfModemCheckConnectionInSeconds;

        private readonly int notificationClosingFrequencyInMinutes;

        private bool IsModemConnected;

        private readonly ILogger logger;

        public NotificationService(INotificationManager notificationManager, int notificationCreationFrequencyInMinutes, int notificationSendingFrequencyInMinutes,
            IModem modem, bool sendAndReceiveSms, int notificationClosingFrequencyInMinutes, int periodOfModemCheckConnectionInSeconds, IPersonContactRepository personContactRepository,
            int delayStartForNotificationTimersInSeconds, ILogger logger)
        {            
            this.notificationManager = notificationManager;
            this.logger = logger;
            this.notificationCreationFrequencyInMinutes = notificationCreationFrequencyInMinutes;
            this.notificationSendingFrequencyInMinutes = notificationSendingFrequencyInMinutes;
            this.notificationClosingFrequencyInMinutes = notificationClosingFrequencyInMinutes;                        
            this.personContactRepository = personContactRepository;
            this.sendAndReceiveSms = sendAndReceiveSms;
            this.modem = modem;            
            this.periodOfModemCheckConnectionInSeconds = periodOfModemCheckConnectionInSeconds;
            this.delayStartForNotificationTimersInSeconds = delayStartForNotificationTimersInSeconds;
        }

        public void StartService()
        {
            this.creationTimer = new Timer(this.CreateNotifications, this, TimeSpan.FromSeconds(this.delayStartForNotificationTimersInSeconds),
                TimeSpan.FromMinutes(this.notificationCreationFrequencyInMinutes));
            this.closingTimer = new Timer(this.CloseNotifications, this, TimeSpan.FromSeconds(this.delayStartForNotificationTimersInSeconds),
                TimeSpan.FromMinutes(this.notificationClosingFrequencyInMinutes));

            if (!this.sendAndReceiveSms)
            {
                return;
            }

            this.InitializeModem();
            this.sendingTimer = new Timer(this.SendNotifications, this, TimeSpan.FromSeconds(this.delayStartForNotificationTimersInSeconds),
                TimeSpan.FromMinutes(this.notificationSendingFrequencyInMinutes));
            this.modemTimer = new Timer(this.CheckModemConnection, this, TimeSpan.FromSeconds(this.delayStartForNotificationTimersInSeconds), TimeSpan.FromSeconds(this.periodOfModemCheckConnectionInSeconds));
        }        

        public void StopService()
        {
            this.creationTimer.Dispose();
            this.closingTimer.Dispose();
            if (!this.sendAndReceiveSms)
            {
                return;
            }

            this.sendingTimer.Dispose();
            this.modemTimer.Dispose();
        }

        private void CreateNotifications(object state)
        {
            this.notificationManager.CreateNewNotifications();
        }

        private void CloseNotifications(object state)
        {
            this.notificationManager.CloseNonAnsweredNotifications();
        }

        private void SendNotifications(object state)
        {
            this.SendMedicamentNotifications();
            this.SendMeasuringNotifications();     
        }

        private void SendMedicamentNotifications()
        {
            while (true)
            {
                if (!this.IsModemConnected)
                {
                    Thread.Sleep(10000);
                    continue;
                }

                var notifications = this.notificationManager.GetNotificationsForSending();
                foreach (var notification in notifications)
                {
                    var personContacts = this.personContactRepository.GetEntitiesByQuery(
                        v => v.PersonId == notification.PersonId && v.ContactType.Title == "Mobile");

                    foreach (var personContact in personContacts)
                    {
                        if (this.modem.SendSms(personContact.Value, notification.Text))
                        {
                            this.logger.LogMessage(string.Format("Message \"{0}\" was sended to mobile {1}", notification.Text, personContact.Value));
                            continue;
                        }

                        this.logger.LogMessage(string.Format("Message \"{0}\" was not sended to mobile {1}", notification.Text, personContact.Value));
                    }
                }

                break;
            }   
        }

        private void SendMeasuringNotifications()
        {
            while (true)
            {
                if (!this.IsModemConnected)
                {
                    Thread.Sleep(10000);
                    continue;
                }

                var notifications = this.notificationManager.GetMeasuringNotificationsForSending();
                foreach (var notification in notifications)
                {
                    var personContacts = this.personContactRepository.GetEntitiesByQuery(
                        v => v.PersonId == notification.PersonId && v.ContactType.Title == "Mobile");

                    foreach (var personContact in personContacts)
                    {
                        if (this.modem.SendSms(personContact.Value, notification.Text))
                        {
                            this.logger.LogMessage(string.Format("Message \"{0}\" was sended to mobile {1}", notification.Text, personContact.Value));
                            continue;
                        }

                        this.logger.LogMessage(string.Format("Message \"{0}\" was not sended to mobile {1}", notification.Text, personContact.Value));
                    }
                }

                break;
            }   
        }

        private void InitializeModem()
        {
            this.IsModemConnected = this.modem.Initialize();
            if (!this.IsModemConnected)
            {
                this.logger.LogMessage("Modem was not initialized");
                return;
            }

            this.logger.LogMessage("Modem was initialized");
        }

        private void CheckModemConnection(object state)
        {            
            if (!this.modem.CheckConnection())
            {
                this.InitializeModem();
            }
        }
    }
}
