namespace DataLayer.Persistence.Group
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Group;    

    public class GroupRepository : IGroupRepository
    {
        private readonly string ConnectionString;

        public GroupRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Group> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Groups.Include("PersonGroups");
        }

        public Group GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Groups.Include("PersonGroups").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Group> GetEntitiesByQuery(Func<Group, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Groups.Include("PersonGroups").Where(query).ToList();
            }  
        }

        public Group CreateOrUpdateEntity(Group entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Groups.Add(entity);
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
                var group = context.Groups.FirstOrDefault(v => v.Id == id);
                if (group == null)
                {
                    return;
                }

                context.Groups.Remove(group);
                context.SaveChanges();
            }
        }        
    }
}
