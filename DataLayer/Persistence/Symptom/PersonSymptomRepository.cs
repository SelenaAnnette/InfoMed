namespace DataLayer.Persistence.Symptom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Symptom;

    public class PersonSymptomRepository : IPersonSymptomRepository
    {
        private readonly string ConnectionString;

        public PersonSymptomRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonSymptom> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.PersonSymptoms;
        }

        public PersonSymptom GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonSymptoms.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonSymptom> GetEntitiesByQuery(Func<PersonSymptom, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonSymptoms.Where(query).ToList();
            }                                    
        }

        public PersonSymptom CreateOrUpdateEntity(PersonSymptom entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonSymptoms.Add(entity);
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
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                var personSymptom = context.PersonSymptoms.FirstOrDefault(v => v.Id == id);
                if (personSymptom == null)
                {
                    return;
                }

                context.PersonSymptoms.Remove(personSymptom);
                context.SaveChanges();
            }
        }
    }
}
