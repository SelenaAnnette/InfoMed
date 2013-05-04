namespace DataLayer.Persistence.AllergicReaction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.AllergicReaction;

    public class AllergicReactionRepository : IAllergicReactionRepository
    {
        private readonly string ConnectionString;

        public AllergicReactionRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AllergicReaction> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AllergicReactions.Include("PersonAllergicReactions");
        }

        public AllergicReaction GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AllergicReactions.Include("PersonAllergicReactions").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<AllergicReaction> GetEntitiesByQuery(Func<AllergicReaction, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AllergicReactions.Include("PersonAllergicReactions").Where(query).ToList();
            }                                    
        }

        public AllergicReaction CreateOrUpdateEntity(AllergicReaction entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.AllergicReactions.Add(entity);
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
                var allergicReaction = context.AllergicReactions.FirstOrDefault(v => v.Id == id);
                if (allergicReaction == null)
                {
                    return;
                }

                context.AllergicReactions.Remove(allergicReaction);
                context.SaveChanges();
            }
        }
    }
}
