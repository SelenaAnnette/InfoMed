namespace DataLayer.Persistence.Person
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Person;

    public class PersonPersonRepository : IPersonPersonRepository
    {
        private readonly string ConnectionString;

        public PersonPersonRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonPerson> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonPersons.Include("FirstPerson").Include("SecondPerson");
        }

        public PersonPerson GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonPersons.Include("FirstPerson").Include("SecondPerson").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonPerson> GetEntitiesByQuery(Func<PersonPerson, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonPersons.Include("FirstPerson").Include("SecondPerson").Where(query).ToList();
            }                                    
        }

        public PersonPerson CreateOrUpdateEntity(PersonPerson entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonPersons.Add(entity);
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
                var personPerson = context.PersonPersons.FirstOrDefault(v => v.Id == id);
                if (personPerson == null)
                {
                    return;
                }

                context.PersonPersons.Remove(personPerson);
                context.SaveChanges();
            }
        }
    }
}
