namespace DataLayer.Persistence.Symptom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Symptom;

    public class AssignedSymptomRepository : IAssignedSymptomRepository
    {
        private readonly string ConnectionString;

        public AssignedSymptomRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AssignedSymptom> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AssignedSymptoms.Include("Person").Include("Symptom");
        }

        public AssignedSymptom GetEntityById(Guid id)
        {
            throw new NotImplementedException("This method is not implemented");
        }

        public IEnumerable<AssignedSymptom> GetEntitiesByQuery(Func<AssignedSymptom, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedSymptoms.Include("Person").Include("Symptom").Where(query).ToList();
            }                                    
        }

        public AssignedSymptom CreateOrUpdateEntity(AssignedSymptom entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (!this.GetEntitiesByQuery(v => v.PersonId == entity.PersonId && v.SymptomId == entity.SymptomId).Any())
                {
                    context.AssignedSymptoms.Add(entity);
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
