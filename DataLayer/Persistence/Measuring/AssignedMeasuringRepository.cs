namespace DataLayer.Persistence.Measuring
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using DataLayer.Persistence.Measuring;

    using Domain.Measuring;

    public class AssignedMeasuringRepository : IAssignedMeasuringRepository
    {
        private readonly string ConnectionString;

        public AssignedMeasuringRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AssignedMeasuring> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AssignedMeasurings.Include("PersonConsultation").Include("MeasuringType");
        }

        public AssignedMeasuring GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedMeasurings.Include("PersonConsultation").Include("MeasuringType")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<AssignedMeasuring> GetEntitiesByQuery(Func<AssignedMeasuring, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedMeasurings.Include("PersonConsultation").Include("MeasuringType")
                    .Where(query).ToList();
            }                                    
        }

        public AssignedMeasuring CreateOrUpdateEntity(AssignedMeasuring entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.AssignedMeasurings.Add(entity);
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
                var assignedMeasuring = context.AssignedMeasurings.FirstOrDefault(v => v.Id == id);
                if (assignedMeasuring == null)
                {
                    return;
                }

                context.AssignedMeasurings.Remove(assignedMeasuring);
                context.SaveChanges();
            }
        }
    }
}
