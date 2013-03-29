﻿namespace TestConsole
{
    using System;
    using System.Linq;

    using DataLayer.Persistence.Group;
    using DataLayer.Persistence.Medicament;
    using DataLayer.Persistence.Person;
    using DataLayer.Persistence.Symptom;
    using DataLayer.Persistence.Measuring;

    using Ninject;

    class Program
    {
        static void Main(string[] args)
        {
            var personRepo = Binder.NinjectKernel.Get<IPersonRepository>();
            var person = personRepo.GetAll().ToList().Last();
            var medicamentRepo = Binder.NinjectKernel.Get<IMedicamentRepository>();
            var medicament = medicamentRepo.GetAll().ToList().First();
            var assignedMedicamentRepo = Binder.NinjectKernel.Get<IAssignedMedicamentRepository>();
            var asignedMedicamentFactory = new AssignedMedicamentFactory();
            var assignedMedicament = asignedMedicamentFactory.Create(person.Id, medicament.Id, 1, "доза", 0.33);
            assignedMedicamentRepo.CreateOrUpdateEntity(assignedMedicament);
            var foundMedicament = assignedMedicamentRepo.GetEntitiesByQuery(v => v.PersonId == person.Id && v.MedicamentId == medicament.Id);
//            var personFactory = new PersonFactory();
//            var person = personFactory.Create(Guid.NewGuid());
//            person.FirstName = "FirstName";
//            person.LastName = "LastName";
//            person.MiddleName = "MiddleName";
//            personRepo.CreateOrUpdateEntity(person);
//            var groupRepo = Binder.NinjectKernel.Get<IGroupRepository>();
//            var groupFactory = new GroupFactory();
//            var group = groupFactory.Create(Guid.NewGuid());
//            group.Name = "Admins";
//            groupRepo.CreateOrUpdateEntity(group);
//            var foundGroup = groupRepo.GetEntitiesByQuery(v => v.Name == "Admins").ToList();
//            var symptomRepo = Binder.NinjectKernel.Get<ISymptomRepository>();
//            var symptomFactory = new SymptomFactory();
//            var symptom = symptomFactory.Create(Guid.NewGuid());
//            symptom.Name = "Silnaya Bol";
//            symptomRepo.CreateOrUpdateEntity(symptom);
//            var foundSymptoms = symptomRepo.GetAll().ToList();
            Console.ReadKey();
//            var measuringTypeRep = Binder.NinjectKernel.Get<IMeasuringTypeRepository>();
//            var measuringType = measuringTypeRep.GetEntitiesByQuery(m => m.Title == "САД").FirstOrDefault();
//            var personMeasuringRep = Binder.NinjectKernel.Get<IPersonMeasuringRepository>();
//            var personMeasuringFactory = new PersonMeasuringFactory();
//            var personMeasuring = personMeasuringFactory.Create(Guid.NewGuid(),measuringType.Id,Guid.NewGuid(),DateTime.Now,120);
//            personMeasuringRep.CreateOrUpdateEntity(personMeasuring);

        }
    }
}
