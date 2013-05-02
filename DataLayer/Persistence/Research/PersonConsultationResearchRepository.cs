namespace DataLayer.Persistence.Research
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Research;

    public class PersonConsultationResearchRepository : IPersonConsultationResearchRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationResearchRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationResearch> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationResearches.Include("Research").Include("PersonConsultation");
        }

        public PersonConsultationResearch GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationResearches.Include("Research").Include("PersonConsultation").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationResearch> GetEntitiesByQuery(Func<PersonConsultationResearch, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationResearches.Include("Research").Include("PersonConsultation").Where(query).ToList();
            }                                    
        }

        public PersonConsultationResearch CreateOrUpdateEntity(PersonConsultationResearch entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationResearches.Add(entity);
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
                var personConsultationResearch = context.PersonConsultationResearches.FirstOrDefault(v => v.Id == id);
                if (personConsultationResearch == null)
                {
                    return;
                }

                context.PersonConsultationResearches.Remove(personConsultationResearch);
                context.SaveChanges();
            }
        }
    }
}
