namespace DataLayer.Persistence.Measuring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Measuring;

    public class MeasuringTypeRepository : IMeasuringTypeRepository
    {
        private readonly string ConnectionString;

        public MeasuringTypeRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<MeasuringType> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.MeasuringTypes;
        }

        public MeasuringType GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MeasuringTypes.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<MeasuringType> GetEntitiesByQuery(Func<MeasuringType, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MeasuringTypes.Where(query);
            }                                    
        }

        public MeasuringType CreateOrUpdateEntity(MeasuringType entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.MeasuringTypes.Add(entity);
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
                var measuringType = context.MeasuringTypes.FirstOrDefault(v => v.Id == id);
                if (measuringType == null)
                {
                    return;
                }

                context.MeasuringTypes.Remove(measuringType);
                context.SaveChanges();
            }
        }
    }
}
