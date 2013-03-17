namespace DataLayer.Persistence.Person
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Person;

    public class ContactTypeRepository : IContactTypeRepository
    {
        private readonly string ConnectionString;

        public ContactTypeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<ContactType> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.ContactTypes.Include("PersonContacts");
        }

        public ContactType GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.ContactTypes.Include("PersonContacts").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<ContactType> GetEntitiesByQuery(Func<ContactType, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.ContactTypes.Include("PersonContacts").Where(query);
            }                                    
        }

        public ContactType CreateOrUpdateEntity(ContactType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.ContactTypes.Add(entity);
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
                var contactType = context.ContactTypes.FirstOrDefault(v => v.Id == id);
                if (contactType == null)
                {
                    return;
                }

                context.ContactTypes.Remove(contactType);
                context.SaveChanges();
            }
        }
    }
}
