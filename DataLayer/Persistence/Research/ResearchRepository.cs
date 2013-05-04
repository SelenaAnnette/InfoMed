namespace DataLayer.Persistence.Research
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Research;

    public class ResearchRepository : IResearchRepository
    {
        private readonly string ConnectionString;

        public ResearchRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Research> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Researches.Include("PersonConsultationResearches");
        }

        public Research GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Researches.Include("PersonConsultationResearches").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Research> GetEntitiesByQuery(Func<Research, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Researches.Include("PersonConsultationResearches").Where(query).ToList();
            }                                    
        }

        public Research CreateOrUpdateEntity(Research entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Researches.Add(entity);
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
                var research = context.Researches.FirstOrDefault(v => v.Id == id);
                if (research == null)
                {
                    return;
                }

                context.Researches.Remove(research);
                context.SaveChanges();
            }
        }
    }
}
