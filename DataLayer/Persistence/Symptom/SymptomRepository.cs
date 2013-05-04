namespace DataLayer.Persistence.Symptom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using DataLayer.Persistence.Person;

    using Domain.Symptom;

    public class SymptomRepository : ISymptomRepository
    {
        private readonly string ConnectionString;

        public SymptomRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Symptom> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Symptoms.Include("AssignedSymptoms").Include("PersonConsultationSymptoms");
        }

        public Symptom GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Symptoms.Include("AssignedSymptoms").Include("PersonConsultationSymptoms")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Symptom> GetEntitiesByQuery(Func<Symptom, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Symptoms.Include("AssignedSymptoms").Include("PersonConsultationSymptoms")
                    .Where(query).ToList();
            }                                    
        }

        public Symptom CreateOrUpdateEntity(Symptom entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Symptoms.Add(entity);
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
                var symptom = context.Symptoms.FirstOrDefault(v => v.Id == id);
                if (symptom == null)
                {
                    return;
                }

                context.Symptoms.Remove(symptom);
                context.SaveChanges();
            }
        }
    }
}
