namespace DataLayer.Persistence.Person
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Person;

    public class CredentialsRepository : ICredentialsRepository
    {
        private readonly string ConnectionString;

        public CredentialsRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Credentials> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Credentials.Include("Person");
        }

        public Credentials GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Credentials.Include("Person").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Credentials> GetEntitiesByQuery(Func<Credentials, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Credentials.Include("Person").Where(query).ToList();
            }                                    
        }

        public Credentials CreateOrUpdateEntity(Credentials entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Credentials.Add(entity);
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
                var credentials = context.Credentials.FirstOrDefault(v => v.Id == id);
                if (credentials == null)
                {
                    return;
                }

                context.Credentials.Remove(credentials);
                context.SaveChanges();
            }
        }
    }
}
