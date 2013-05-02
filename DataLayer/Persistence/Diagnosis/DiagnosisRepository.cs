namespace DataLayer.Persistence.Diagnosis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Diagnosis;

    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly string ConnectionString;

        public DiagnosisRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Diagnosises.Include("PersonConsultationDiagnosises");
        }

        public Diagnosis GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Diagnosises.Include("PersonConsultationDiagnosises").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Diagnosis> GetEntitiesByQuery(Func<Diagnosis, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Diagnosises.Include("PersonConsultationDiagnosises").Where(query).ToList();
            }                                    
        }

        public Diagnosis CreateOrUpdateEntity(Diagnosis entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Diagnosises.Add(entity);
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
                var diagnosis = context.Diagnosises.FirstOrDefault(v => v.Id == id);
                if (diagnosis == null)
                {
                    return;
                }

                context.Diagnosises.Remove(diagnosis);
                context.SaveChanges();
            }
        }
    }
}
