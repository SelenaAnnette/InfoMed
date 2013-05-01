namespace DataLayer.Persistence.AllergicReaction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.AllergicReaction;

    public class PersonAllergicReactionRepository : IPersonAllergicReactionRepository
    {
        private readonly string ConnectionString;

        public PersonAllergicReactionRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonAllergicReaction> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonAllergicReactions.Include("Person").Include("AllergicReaction");
        }

        public PersonAllergicReaction GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonAllergicReactions.Include("Person").Include("AllergicReaction").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonAllergicReaction> GetEntitiesByQuery(Func<PersonAllergicReaction, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonAllergicReactions.Include("Person").Include("AllergicReaction").Where(query).ToList();
            }                                    
        }

        public PersonAllergicReaction CreateOrUpdateEntity(PersonAllergicReaction entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonAllergicReactions.Add(entity);
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
                var personAllergicReaction = context.PersonAllergicReactions.FirstOrDefault(v => v.Id == id);
                if (personAllergicReaction == null)
                {
                    return;
                }

                context.PersonAllergicReactions.Remove(personAllergicReaction);
                context.SaveChanges();
            }
        }
    }
}
