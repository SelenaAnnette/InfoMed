namespace TestConsole
{
    using System;
    using System.Linq;    

    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;

    using Ninject;

    using ServerLogic.Notification;

    using SmsModule;

    class Program
    {
        static void Main(string[] args)
        {
            var personRepository = Binder.NinjectKernel.Get<IPersonRepository>();
            var personFactory = new PersonFactory();
            var person = personFactory.Create(Guid.NewGuid(), "TestPerson", "TestPerson", "TestPerson");
            personRepository.CreateOrUpdateEntity(person);
            Console.WriteLine("Person was created");

            var medicamentRepository = Binder.NinjectKernel.Get<IMedicamentRepository>();
            var medicamentFactory = new MedicamentFactory();
            var medicament = medicamentFactory.Create(Guid.NewGuid(), "TestMedicament", "TestMedicamentCode");
            medicamentRepository.CreateOrUpdateEntity(medicament);
            Console.WriteLine("Medicament was created");

            var assignedMedicamentRepository = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
            var assignedMedicamentFactory = new AssignedMedicamentFactory();
            var assignedMedicament = assignedMedicamentFactory.Create(person.Id, medicament.Id, 1, "TestDosa", 0.33);
            assignedMedicamentRepository.CreateOrUpdateEntity(assignedMedicament);
            Console.WriteLine("Medicament was assigned");

            var notificationRepository = Binder.NinjectKernel.Get<INotificationRepository>();
            var notificationFactory = new NotificationFactory();
            var notification = notificationFactory.Create(Guid.NewGuid(), person.Id, medicament.Id, DateTime.Now, "TestNotificationMessage");
            notificationRepository.CreateOrUpdateEntity(notification);
            Console.WriteLine("Notification was created");

            var contactTypeRepository = Binder.NinjectKernel.Get<IContactTypeRepository>();
            var contactType = contactTypeRepository.GetEntitiesByQuery(v => v.Title == "Mobile").First();

            var personContactRepository = Binder.NinjectKernel.Get<IPersonContactRepository>();
            var personContactFactory = new PersonContactFactory();
            var personContact = personContactFactory.Create(Guid.NewGuid(), person.Id, contactType.Id, "89042114372");
            personContactRepository.CreateOrUpdateEntity(personContact);
            Console.WriteLine("Person mobile contact was created");
            
//            var notificationManager = Binder.NinjectKernel.Get<INotificationManager>();            
            try
            {
//                notificationManager.SendAllActiveNotifications();
//                Console.WriteLine("Notification was sended");
                var modem = Binder.NinjectKernel.Get<IModem>();
                modem.Initialize();
                if (modem.SendSms("89042114372", "Test sms"))
                {
                    Console.WriteLine("Message is sent");
                }
                else
                {
                    Console.WriteLine("Message is not sent");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Modem error");
                Console.WriteLine(ex.Message);                
            }
            finally
            {
                Console.ReadKey();
            }            
        }
    }
}
