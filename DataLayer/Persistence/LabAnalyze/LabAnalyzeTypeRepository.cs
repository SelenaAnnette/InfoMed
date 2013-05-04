namespace DataLayer.Persistence.LabAnalyze
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.LabAnalyze;

    public class LabAnalyzeTypeRepository : ILabAnalyzeTypeRepository
    {
        private readonly string ConnectionString;

        public LabAnalyzeTypeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<LabAnalyzeType> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.LabAnalyzeTypes.Include("PersonConsultationLabAnalyzes");
        }

        public LabAnalyzeType GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.LabAnalyzeTypes.Include("PersonConsultationLabAnalyzes").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<LabAnalyzeType> GetEntitiesByQuery(Func<LabAnalyzeType, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.LabAnalyzeTypes.Include("PersonConsultationLabAnalyzes").Where(query).ToList();
            }                                    
        }

        public LabAnalyzeType CreateOrUpdateEntity(LabAnalyzeType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.LabAnalyzeTypes.Add(entity);
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
                var labAnalyzeType = context.LabAnalyzeTypes.FirstOrDefault(v => v.Id == id);
                if (labAnalyzeType == null)
                {
                    return;
                }

                context.LabAnalyzeTypes.Remove(labAnalyzeType);
                context.SaveChanges();
            }
        }
    }
}
