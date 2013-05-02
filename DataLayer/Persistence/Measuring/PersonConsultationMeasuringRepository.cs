namespace DataLayer.Persistence.Measuring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Measuring;

    public class PersonConsultationMeasuringRepository : IPersonConsultationMeasuringRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationMeasuringRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationMeasuring> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationMeasurings.Include("PersonConsultation").Include("MeasuringType");
        }

        public PersonConsultationMeasuring GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationMeasurings.Include("PersonConsultation").Include("MeasuringType")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationMeasuring> GetEntitiesByQuery(Func<PersonConsultationMeasuring, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationMeasurings.Include("PersonConsultation").Include("MeasuringType")
                    .Where(query).ToList();
            }                                    
        }

        public PersonConsultationMeasuring CreateOrUpdateEntity(PersonConsultationMeasuring entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationMeasurings.Add(entity);
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
                var personConsultationMeasuring = context.PersonConsultationMeasurings.FirstOrDefault(v => v.Id == id);
                if (personConsultationMeasuring == null)
                {
                    return;
                }

                context.PersonConsultationMeasurings.Remove(personConsultationMeasuring);
                context.SaveChanges();
            }
        }
    }
}
