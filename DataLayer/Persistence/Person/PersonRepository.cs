namespace DataLayer.Persistence.Person
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Person;

    public class PersonRepository : IPersonRepository
    {
        private readonly string ConnectionString;

        public PersonRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Persons.Include("AssignedSymptoms").Include("FirstPersonPersons").Include("SecondPersonPersons").Include("PersonContacts").Include("AssignedRiskFactors")
                .Include("Credentials").Include("PersonGroups").Include("PersonOperations").Include("PersonDiseases").Include("PersonAllergicReactions")
                .Include("ConsultationsAsDoctor").Include("ConsultationsAsPatient").Include("PersonHospitalizations");
        }

        public Person GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Persons.Include("AssignedSymptoms").Include("FirstPersonPersons").Include("SecondPersonPersons").Include("PersonContacts").Include("AssignedRiskFactors")
                    .Include("Credentials").Include("PersonGroups").Include("PersonOperations").Include("PersonDiseases").Include("PersonAllergicReactions")
                    .Include("ConsultationsAsDoctor").Include("ConsultationsAsPatient").Include("PersonHospitalizations")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Person> GetEntitiesByQuery(Func<Person, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Persons.Include("AssignedSymptoms").Include("FirstPersonPersons").Include("SecondPersonPersons").Include("PersonContacts").Include("AssignedRiskFactors")
                    .Include("Credentials").Include("PersonGroups").Include("PersonOperations").Include("PersonDiseases").Include("PersonAllergicReactions")
                    .Include("ConsultationsAsDoctor").Include("ConsultationsAsPatient").Include("PersonHospitalizations")
                    .Where(query).ToList();
            }                                    
        }

        public Person CreateOrUpdateEntity(Person entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Persons.Add(entity);
                }
                else
                {
                    context.Entry(entity).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

            return entity;
        }

        public void DeleteEntity(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                var person = context.Persons.FirstOrDefault(v => v.Id == id);
                if (person == null)
                {
                    return;
                }

                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }
    }
}
