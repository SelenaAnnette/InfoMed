namespace ServerLogic.Sms
{
    using System;
    using System.Linq;
    using System.Threading;

    using DataLayer.Persistence.Measuring;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;
    using DataLayer.Persistence.Sms;

    using SmsModule;

    public class SmsManager : ISmsManager
    {
        private readonly IDomainSmsRepository domainSmsRepository;

        private readonly IPersonRepository personRepository;

        private readonly IPersonContactRepository personContactRepository;

        private readonly IAssignedMeasuringRepository assignedMeasuringRepository;

        private readonly IMeasuringNotificationRepository measuringNotificationRepository;

        private readonly IAssignedMedicamentRepository assignedMedicamentRepository;

        private readonly INotificationRepository notificationRepository;

        private readonly IPersonMeasuringRepository personMeasuringRepository;

        private readonly IPersonMedicamentRepository personMedicamentRepository;

        private readonly DomainSmsFactory domainSmsFactory;

        private readonly PersonMeasuringFactory personMeasuringFactory;

        private readonly PersonMedicamentFactory personMedicamentFactory;

        public SmsManager(IDomainSmsRepository domainSmsRepository, IPersonRepository personRepository, IPersonContactRepository personContactRepository,
            IAssignedMeasuringRepository assignedMeasuringRepository, IMeasuringNotificationRepository measuringNotificationRepository,
            IAssignedMedicamentRepository assignedMedicamentRepository, INotificationRepository notificationRepository, IPersonMeasuringRepository personMeasuringRepository,
            IPersonMedicamentRepository personMedicamentRepository)
         {
             this.domainSmsRepository = domainSmsRepository;
             this.personRepository = personRepository;
             this.personContactRepository = personContactRepository;
             this.assignedMeasuringRepository = assignedMeasuringRepository;
             this.measuringNotificationRepository = measuringNotificationRepository;
             this.assignedMedicamentRepository = assignedMedicamentRepository;
             this.notificationRepository = notificationRepository;
             this.personMeasuringRepository = personMeasuringRepository;
             this.personMedicamentRepository = personMedicamentRepository;
             this.domainSmsFactory = new DomainSmsFactory();
             this.personMeasuringFactory = new PersonMeasuringFactory();
             this.personMedicamentFactory = new PersonMedicamentFactory();
         }

        public void SaveNewSmses(Sms[] smses)
        {
            foreach (var sms in smses)
            {
                if (sms == null)
                {
                    continue;    
                }

                var domainSms = this.domainSmsRepository.GetEntitiesByQuery(v => v.SenderNumber == sms.From && v.SendingDate == sms.DTime && v.Text == sms.Text).FirstOrDefault();
                if (domainSms != null)
                {
                    continue;
                }

                var newDomainSms = this.domainSmsFactory.Create(Guid.NewGuid(), sms.From, sms.DTime, sms.Text);
                this.domainSmsRepository.CreateOrUpdateEntity(newDomainSms);
            }
        }

        public void CheckSmsForNotifications()
        {
            var domainSmses = this.domainSmsRepository.GetEntitiesByQuery(v => !v.IsRead).ToList();
            domainSmses.ForEach(v =>
                {
                    var personContact = this.personContactRepository.GetEntitiesByQuery(p => p.ContactType.Title == "Mobile" && p.Value == v.SenderNumber).FirstOrDefault();
                    if (personContact == null)
                    {
                        return;
                    }

                    var person = this.personRepository.GetEntityById(personContact.PersonId);
                    if (person == null)
                    {
                        return;
                    }

                    var splittedSms = v.Text.Split(' ');
                    if (splittedSms[0].ToLower() == "изм")
                    {
                        var assignedMeasuring = this.assignedMeasuringRepository.GetEntitiesByQuery(m => m.MeasuringType.Code == splittedSms[1] && m.PersonConsultation.PatientId == person.Id).FirstOrDefault();
                        if (assignedMeasuring == null)
                        {
                            return;
                        }

                        double value;
                        try
                        {
                            value = Convert.ToDouble(splittedSms[2]);
                        }
                        catch (Exception ex)
                        {
                            return;
                        }
                        

                        var personMeasuring = this.personMeasuringFactory.Create(Guid.NewGuid(), assignedMeasuring.MeasuringTypeId, person.Id, DateTime.Now, value);
                        this.personMeasuringRepository.CreateOrUpdateEntity(personMeasuring);

                        var measuringNotification = this.measuringNotificationRepository.GetEntitiesByQuery(mn => mn.AssignedMeasuringId == assignedMeasuring.Id && mn.IsActive && mn.PersonId == person.Id).FirstOrDefault();
                        if (measuringNotification == null)
                        {
                            return;
                        }

                        measuringNotification.ExecutedDate = DateTime.Now;
                        measuringNotification.IsActive = false;
                        this.measuringNotificationRepository.CreateOrUpdateEntity(measuringNotification);
                    }
                    if (splittedSms[0].ToLower() == "преп")
                    {
                        var test = this.assignedMedicamentRepository.GetEntitiesByQuery(m => m.Medicament.Code == splittedSms[1]);
                        var test2 = this.assignedMedicamentRepository.GetEntitiesByQuery(m => m.PersonConsultation.PatientId == person.Id);
                        var assignedMedicament = this.assignedMedicamentRepository.GetEntitiesByQuery(m => m.Medicament.Code == splittedSms[1] && m.PersonConsultation.PatientId == person.Id).FirstOrDefault();
                        if (assignedMedicament == null)
                        {
                            return;
                        }

                        var personMedicament = this.personMedicamentFactory.Create(Guid.NewGuid(), assignedMedicament.MedicamentId, person.Id, DateTime.Now);
                        this.personMedicamentRepository.CreateOrUpdateEntity(personMedicament);

                        var notification = this.notificationRepository.GetEntitiesByQuery(mn => mn.AssignedMedicamentId == assignedMedicament.Id && mn.IsActive && mn.PersonId == person.Id).FirstOrDefault();
                        if (notification == null)
                        {
                            return;
                        }

                        notification.ExecutedDate = DateTime.Now;
                        notification.IsActive = false;
                        this.notificationRepository.CreateOrUpdateEntity(notification);
                    }

                    v.IsRead = true;
                    this.domainSmsRepository.CreateOrUpdateEntity(v);
                });
        }
    }
}