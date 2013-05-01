namespace DataLayer.Persistence.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Operation;

    public class PersonOperationRepository : IPersonOperationRepository
    {
        private readonly string ConnectionString;

        public PersonOperationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonOperation> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonOperations.Include("Person").Include("Operation");
        }

        public PersonOperation GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonOperations.Include("Person").Include("Operation").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonOperation> GetEntitiesByQuery(Func<PersonOperation, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonOperations.Include("Person").Include("Operation").Where(query).ToList();
            }                                    
        }

        public PersonOperation CreateOrUpdateEntity(PersonOperation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonOperations.Add(entity);
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
                var personOperation = context.PersonOperations.FirstOrDefault(v => v.Id == id);
                if (personOperation == null)
                {
                    return;
                }

                context.PersonOperations.Remove(personOperation);
                context.SaveChanges();
            }
        }
    }
}
