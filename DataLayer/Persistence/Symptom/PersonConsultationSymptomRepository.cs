namespace DataLayer.Persistence.Symptom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Symptom;

    public class PersonConsultationSymptomRepository : IPersonConsultationSymptomRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationSymptomRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationSymptom> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationSymptoms.Include("Symptom").Include("PersonConsultation");
        }

        public PersonConsultationSymptom GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationSymptoms.Include("Symptom").Include("PersonConsultation")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationSymptom> GetEntitiesByQuery(Func<PersonConsultationSymptom, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationSymptoms.Include("Symptom").Include("PersonConsultation")
                    .Where(query).ToList();
            }                                    
        }

        public PersonConsultationSymptom CreateOrUpdateEntity(PersonConsultationSymptom entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationSymptoms.Add(entity);
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
                var personConsultationSymptom = context.PersonConsultationSymptoms.FirstOrDefault(v => v.Id == id);
                if (personConsultationSymptom == null)
                {
                    return;
                }

                context.PersonConsultationSymptoms.Remove(personConsultationSymptom);
                context.SaveChanges();
            }
        }
    }
}
