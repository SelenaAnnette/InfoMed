﻿namespace TestConsole
{
    using System;
    using System.Linq;

    using DataLayer.Persistence.Consultation;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;

    using Domain.Person;

    using Ninject;

    using ServerLogic.Logger;

    using SmsModule;

    class Program
    {
        private static readonly ILogger Logger  = Binder.NinjectKernel.Get<ILogger>();
        static void Main(string[] args)
        {            
            //MainTest();
            //PersonTest();
            PersonContactTest();
            Console.WriteLine("Tests are completed");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    
        private static void PersonTest()
        {
            var personRepository = Binder.NinjectKernel.Get<IPersonRepository>();
            var personFactory = new PersonFactory();
            var person = personFactory.Create(Guid.NewGuid(), "TestPerson", "TestPerson", "TestPerson", DateTime.Now, Sex.Man);
            personRepository.CreateOrUpdateEntity(person);
            Console.WriteLine("Person was created");

            person.FirstName = "Test1Person";
            personRepository.CreateOrUpdateEntity(person);
            Console.WriteLine("Person was updated");

            person = personRepository.GetEntityById(person.Id);
            Console.WriteLine(person.FirstName);

            personRepository.DeleteEntity(person.Id);
            Console.WriteLine("Person was deleted");
        }

        private static void PersonContactTest()
        {
            var personRepository = Binder.NinjectKernel.Get<IPersonRepository>();
            var personFactory = new PersonFactory();
            var person = personFactory.Create(Guid.NewGuid(), "TestPerson", "TestPerson", "TestPerson", DateTime.Now, Sex.Man);
            personRepository.CreateOrUpdateEntity(person);
            Console.WriteLine("Person was created");

            var contactTypeRepository = Binder.NinjectKernel.Get<IContactTypeRepository>();
            var contactTypeFactory = new ContactTypeFactory();
            var contactType = contactTypeFactory.Create(Guid.NewGuid(), "TestContactType");
            contactTypeRepository.CreateOrUpdateEntity(contactType);
            Console.WriteLine("ContactType was created");

            var personContactRepository = Binder.NinjectKernel.Get<IPersonContactRepository>();
            var personContactFactory = new PersonContactFactory();
            var personContact = personContactFactory.Create(Guid.NewGuid(), person.Id, contactType.Id, "TestValue");
            personContactRepository.CreateOrUpdateEntity(personContact);
            Console.WriteLine("PersonContact was created");

            personContact.Value = "Test1Value";
            personContactRepository.CreateOrUpdateEntity(personContact);
            Console.WriteLine("PersonContact was updated");

            personContact = personContactRepository.GetEntityById(personContact.Id);
            Console.WriteLine(personContact.Value);

            personContactRepository.DeleteEntity(personContact.Id);
            Console.WriteLine("PersonContact was deleted");
            contactTypeRepository.DeleteEntity(contactType.Id);
            Console.WriteLine("ContactType was deleted");
            personRepository.DeleteEntity(person.Id);
            Console.WriteLine("Person was deleted");
        }
    
        private static void MainTest()
        {
            var personRepository = Binder.NinjectKernel.Get<IPersonRepository>();
            var personFactory = new PersonFactory();
            var person = personFactory.Create(Guid.NewGuid(), "TestPerson", "TestPerson", "TestPerson", DateTime.Now, Sex.Man);
            personRepository.CreateOrUpdateEntity(person);
            Console.WriteLine("Person was created");

            var medicamentFormRepository = Binder.NinjectKernel.Get<IMedicamentFormRepository>();
            var medicamentFormFactory = new MedicamentFormFactory();
            var medicamentForm = medicamentFormFactory.Create(Guid.NewGuid(), "TestMedicamentForm", "TestMedicamentFormMeasuring");
            medicamentFormRepository.CreateOrUpdateEntity(medicamentForm);
            Console.WriteLine("MedicamentForm was created");

            var medicamentRepository = Binder.NinjectKernel.Get<IMedicamentRepository>();
            var medicamentFactory = new MedicamentFactory();
            var medicament = medicamentFactory.Create(Guid.NewGuid(), "TestMedicament", "TestMedicamentCode", medicamentForm.Id);
            medicamentRepository.CreateOrUpdateEntity(medicament);
            Console.WriteLine("Medicament was created");

            var consultationTypeRepository = Binder.NinjectKernel.Get<IConsultationTypeRepository>();
            var consultationTypeFactory = new ConsultationTypeFactory();
            var consultationType = consultationTypeFactory.Create(Guid.NewGuid(), "Test consultation type");
            consultationTypeRepository.CreateOrUpdateEntity(consultationType);
            Console.WriteLine("Consultation type was created");

            var personConsultationRepository = Binder.NinjectKernel.Get<IPersonConsultationRepository>();
            var personConsultationFactory = new PersonConsultationFactory();
            var personConsultation = personConsultationFactory.Create(Guid.NewGuid(), person.Id, person.Id, consultationType.Id, DateTime.Now);
            personConsultationRepository.CreateOrUpdateEntity(personConsultation);
            Console.WriteLine("Consultation was created");

            var medicamentApplicationWayRepository = Binder.NinjectKernel.Get<IMedicamentApplicationWayRepository>();
            var medicamentApplicationWayFactory = new MedicamentApplicationWayFactory();
            var medicamentApplicationWay = medicamentApplicationWayFactory.Create(Guid.NewGuid(), "Test medicamentApplicationWay");
            medicamentApplicationWayRepository.CreateOrUpdateEntity(medicamentApplicationWay);
            Console.WriteLine("MedicamentApplicationWay was created");

            var assignedMedicamentRepository = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
            var assignedMedicamentFactory = new AssignedMedicamentFactory();
            var assignedMedicament = assignedMedicamentFactory.Create(Guid.NewGuid(), personConsultation.Id, medicament.Id, medicamentApplicationWay.Id, 1, DateTime.Now, 3, 5, 1);
            assignedMedicamentRepository.CreateOrUpdateEntity(assignedMedicament);
            Console.WriteLine("Medicament was assigned");

            var notificationRepository = Binder.NinjectKernel.Get<INotificationRepository>();
            var notificationFactory = new NotificationFactory();
            var notification = notificationFactory.Create(Guid.NewGuid(), assignedMedicament.Id, person.Id, medicament.Id, DateTime.Now, "Test Notification Message");
            notificationRepository.CreateOrUpdateEntity(notification);
            Console.WriteLine("Notification was created");

            var contactTypeRepository = Binder.NinjectKernel.Get<IContactTypeRepository>();
            var contactType = contactTypeRepository.GetEntitiesByQuery(v => v.Title == "Mobile").First();

            var personContactRepository = Binder.NinjectKernel.Get<IPersonContactRepository>();
            var personContactFactory = new PersonContactFactory();
            var personContact = personContactFactory.Create(Guid.NewGuid(), person.Id, contactType.Id, "89042114372");
            personContactRepository.CreateOrUpdateEntity(personContact);
            Console.WriteLine("Person mobile contact was created");

            try
            {
                var modem = Binder.NinjectKernel.Get<IModem>();
                if (modem.Initialize())
                {
                    if (modem.SendSms(personContact.Value, notification.Text))
                    {
                        Console.WriteLine("Message is sent");
                    }
                    else
                    {
                        Console.WriteLine("Message is not sent");
                    }
                }
                else
                {
                    Console.WriteLine("Modem was not initialized");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Modem error");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                personContactRepository.DeleteEntity(personContact.Id);
                Console.WriteLine("Person mobile contact wad deleted");

                notificationRepository.DeleteEntity(notification.Id);
                Console.WriteLine("Notification wad deleted");

                assignedMedicamentRepository.DeleteEntity(assignedMedicament.Id);
                Console.WriteLine("Assigned medicament wad deleted");

                medicamentRepository.DeleteEntity(medicament.Id);
                Console.WriteLine("Medicament wad deleted");

                personRepository.DeleteEntity(person.Id);
                Console.WriteLine("Person wad deleted");

                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }            
        }
    }
}
