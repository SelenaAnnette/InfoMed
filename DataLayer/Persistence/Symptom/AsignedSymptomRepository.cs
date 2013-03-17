namespace DataLayer.Persistence.Symptom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Symptom;

    public class AsignedSymptomRepository : IAsignedSymptomRepository
    {
        private readonly string ConnectionString;

        public AsignedSymptomRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AsignedSymptom> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonSymptoms.Include("Person").Include("Symptom");
        }

        public AsignedSymptom GetEntityById(Guid id)
        {
            throw new NotImplementedException("This method is not implemented");
        }

        public IEnumerable<AsignedSymptom> GetEntitiesByQuery(Func<AsignedSymptom, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonSymptoms.Include("Person").Include("Symptom").Where(query);
            }                                    
        }

        public AsignedSymptom CreateOrUpdateEntity(AsignedSymptom entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntitiesByQuery(v => v.PersonId == entity.PersonId && v.SymptomId == entity.SymptomId) == null)
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
            throw new NotImplementedException("This method is not implemented");
        }
    }
}
