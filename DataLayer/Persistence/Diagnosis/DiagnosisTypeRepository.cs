namespace DataLayer.Persistence.Diagnosis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Diagnosis;

    public class DiagnosisTypeRepository : IDiagnosisTypeRepository
    {
        private readonly string ConnectionString;

        public DiagnosisTypeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<DiagnosisType> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.DiagnosisTypes.Include("PersonConsultationDiagnosises");
        }

        public DiagnosisType GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.DiagnosisTypes.Include("PersonConsultationDiagnosises").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<DiagnosisType> GetEntitiesByQuery(Func<DiagnosisType, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.DiagnosisTypes.Include("PersonConsultationDiagnosises").Where(query).ToList();
            }                                    
        }

        public DiagnosisType CreateOrUpdateEntity(DiagnosisType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.DiagnosisTypes.Add(entity);
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
                var diagnosisType = context.DiagnosisTypes.FirstOrDefault(v => v.Id == id);
                if (diagnosisType == null)
                {
                    return;
                }

                context.DiagnosisTypes.Remove(diagnosisType);
                context.SaveChanges();
            }
        }
    }
}
