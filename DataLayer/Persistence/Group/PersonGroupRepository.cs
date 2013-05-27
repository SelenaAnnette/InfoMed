namespace DataLayer.Persistence.Group
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Group;    

    public class PersonGroupRepository : IPersonGroupRepository
    {
        private readonly string ConnectionString;

        public PersonGroupRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonGroup> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonGroups.Include("Person").Include("Group");
        }

        public PersonGroup GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonGroups.Include("Person").Include("Group").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonGroup> GetEntitiesByQuery(Func<PersonGroup, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonGroups.Include("Person").Include("Group").Where(query).ToList();
            }  
        }

        public PersonGroup CreateOrUpdateEntity(PersonGroup entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonGroups.Add(entity);
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
                var personGroups = context.PersonGroups.FirstOrDefault(v => v.Id == id);
                if (personGroups == null)
                {
                    return;
                }

                context.PersonGroups.Remove(personGroups);
                context.SaveChanges();
            }
        }        
    }
}
