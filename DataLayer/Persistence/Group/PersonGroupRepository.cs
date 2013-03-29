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
            throw new NotImplementedException("this method is not implemented");
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
                if (!this.GetEntitiesByQuery(v => v.GroupId == entity.GroupId && v.PersonId == entity.PersonId).Any())
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
            throw new NotImplementedException("this method is not implemented");
        }        
    }
}
