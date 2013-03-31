namespace ServerLogic.Notification
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    using DataLayer.Persistence.Person;

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

        public NotificationService(INotificationManager notificationManager, int notificationCreationFrequencyInMinutes, int notificationSendingFrequencyInMinutes,
            IModem modem, bool sendAndReceiveSms, int notificationClosingFrequencyInMinutes, int periodOfModemCheckConnectionInSeconds, IPersonContactRepository personContactRepository,
            int delayStartForNotificationTimersInSeconds)
        {            
            this.notificationManager = notificationManager;
            this.notificationCreationFrequencyInMinutes = notificationCreationFrequencyInMinutes;
            this.notificationSendingFrequencyInMinutes = notificationSendingFrequencyInMinutes;
            this.notificationClosingFrequencyInMinutes = notificationClosingFrequencyInMinutes;                        
            this.personContactRepository = personContactRepository;
            this.sendAndReceiveSms = sendAndReceiveSms;
            this.modem = modem;            
            this.periodOfModemCheckConnectionInSeconds = periodOfModemCheckConnectionInSeconds;
            this.delayStartForNotificationTimersInSeconds = delayStartForNotificationTimersInSeconds;
        }


        ~NotificationService()
        {
            this.StopService();
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
                        this.modem.SendSms(personContact.Value, notification.Text);
                    }                        
                }

                break;                
            }                            
        }

        private void InitializeModem()
        {
            this.IsModemConnected = this.modem.Initialize();
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
