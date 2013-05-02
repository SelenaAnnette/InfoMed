namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;    
    using System.Data;
    using System.Linq;

    using Domain.Medicament;

    public class AssignedMedicamentMeasuringRepository : IAssignedMedicamentMeasuringRepository
    {
        private readonly string ConnectionString;

        public AssignedMedicamentMeasuringRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AssignedMedicamentMeasuring> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AssignedMedicamentMeasurings.Include("AssignedMedicament").Include("MeasuringType");
        }

        public AssignedMedicamentMeasuring GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedMedicamentMeasurings.Include("AssignedMedicament").Include("MeasuringType")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<AssignedMedicamentMeasuring> GetEntitiesByQuery(Func<AssignedMedicamentMeasuring, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedMedicamentMeasurings.Include("AssignedMedicament").Include("MeasuringType")
                    .Where(query).ToList();
            }                                    
        }

        public AssignedMedicamentMeasuring CreateOrUpdateEntity(AssignedMedicamentMeasuring entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.AssignedMedicamentMeasurings.Add(entity);
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
                var assignedMedicamentMeasuring = context.AssignedMedicamentMeasurings.FirstOrDefault(v => v.Id == id);
                if (assignedMedicamentMeasuring == null)
                {
                    return;
                }

                context.AssignedMedicamentMeasurings.Remove(assignedMedicamentMeasuring);
                context.SaveChanges();
            }
        }
    }
}
