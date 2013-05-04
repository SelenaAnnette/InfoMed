namespace DataLayer.Persistence.LabAnalyze
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.LabAnalyze;

    public class PersonConsultationLabAnalyzeRepository : IPersonConsultationLabAnalyzeRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationLabAnalyzeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationLabAnalyze> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationLabAnalyzes.Include("LabAnalyzeType").Include("PersonConsultation");
        }

        public PersonConsultationLabAnalyze GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationLabAnalyzes.Include("LabAnalyzeType").Include("PersonConsultation").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationLabAnalyze> GetEntitiesByQuery(Func<PersonConsultationLabAnalyze, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationLabAnalyzes.Include("LabAnalyzeType").Include("PersonConsultation").Where(query).ToList();
            }                                    
        }

        public PersonConsultationLabAnalyze CreateOrUpdateEntity(PersonConsultationLabAnalyze entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationLabAnalyzes.Add(entity);
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
                var personConsultationLabAnalyze = context.PersonConsultationLabAnalyzes.FirstOrDefault(v => v.Id == id);
                if (personConsultationLabAnalyze == null)
                {
                    return;
                }

                context.PersonConsultationLabAnalyzes.Remove(personConsultationLabAnalyze);
                context.SaveChanges();
            }
        }
    }
}
