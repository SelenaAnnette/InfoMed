namespace TestConsole
{
    using System;
    using System.Linq;    

    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Message;
    using DataLayer.Persistence.Person;    

    using Ninject;

    using ServerLogic.Logger;

    using SmsModule;

    class Program
    {
        private static readonly ILogger Logger  = Binder.NinjectKernel.Get<ILogger>();
        static void Main(string[] args)
        {            
            MainTest();            
            Console.WriteLine("Tests are completed");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }    
    
        private static void MainTest()
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
            var assignedMedicament = assignedMedicamentFactory.Create(Guid.NewGuid(), person.Id, medicament.Id, 1, "Test dosa", DateTime.Now, 3, 5, 1);
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
