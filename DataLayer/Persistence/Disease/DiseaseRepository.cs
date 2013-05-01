namespace DataLayer.Persistence.Disease
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Disease;

    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly string ConnectionString;

        public DiseaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Disease> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Diseases.Include("PersonDiseases");
        }

        public Disease GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Diseases.Include("PersonDiseases").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Disease> GetEntitiesByQuery(Func<Disease, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Diseases.Include("PersonDiseases").Where(query).ToList();
            }                                    
        }

        public Disease CreateOrUpdateEntity(Disease entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Diseases.Add(entity);
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
                var disease = context.Diseases.FirstOrDefault(v => v.Id == id);
                if (disease == null)
                {
                    return;
                }

                context.Diseases.Remove(disease);
                context.SaveChanges();
            }
        }
    }
}
