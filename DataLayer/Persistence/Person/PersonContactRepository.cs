namespace DataLayer.Persistence.Person
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Person;

    public class PersonContactRepository : IPersonContactRepository
    {
        private readonly string ConnectionString;

        public PersonContactRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonContact> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonContacts.Include("Person").Include("ContactType");
        }

        public PersonContact GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonContacts.Include("Person").Include("ContactType").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonContact> GetEntitiesByQuery(Func<PersonContact, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonContacts.Include("Person").Include("ContactType").Where(query).ToList();
            }                                    
        }

        public PersonContact CreateOrUpdateEntity(PersonContact entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonContacts.Add(entity);
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
                var personContact = context.PersonContacts.FirstOrDefault(v => v.Id == id);
                if (personContact == null)
                {
                    return;
                }

                context.PersonContacts.Remove(personContact);
                context.SaveChanges();
            }
        }
    }
}
