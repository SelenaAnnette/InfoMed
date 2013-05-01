namespace DataLayer.Persistence.Consultation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Consultation;

    public class ConsultationTypeRepository : IConsultationTypeRepository
    {
        private readonly string ConnectionString;

        public ConsultationTypeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<ConsultationType> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.ConsultationTypes.Include("PersonConsultations");
        }

        public ConsultationType GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.ConsultationTypes.Include("PersonConsultations").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<ConsultationType> GetEntitiesByQuery(Func<ConsultationType, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.ConsultationTypes.Include("PersonConsultations").Where(query).ToList();
            }                                    
        }

        public ConsultationType CreateOrUpdateEntity(ConsultationType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.ConsultationTypes.Add(entity);
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
                var consultationType = context.ConsultationTypes.FirstOrDefault(v => v.Id == id);
                if (consultationType == null)
                {
                    return;
                }

                context.ConsultationTypes.Remove(consultationType);
                context.SaveChanges();
            }
        }
    }
}
