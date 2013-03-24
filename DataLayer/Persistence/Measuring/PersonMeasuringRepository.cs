namespace DataLayer.Persistence.Measuring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Measuring;

    public class PersonMeasuringRepository : IPersonMeasuringRepository
    {
        private readonly string ConnectionString;

        public PersonMeasuringRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonMeasuring> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.PersonMeasurings;
        }

        public PersonMeasuring GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonMeasurings.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonMeasuring> GetEntitiesByQuery(Func<PersonMeasuring, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonMeasurings.Where(query).ToList();
            }                                    
        }

        public PersonMeasuring CreateOrUpdateEntity(PersonMeasuring entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonMeasurings.Add(entity);
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
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                var personMeasuring = context.PersonMeasurings.FirstOrDefault(v => v.Id == id);
                if (personMeasuring == null)
                {
                    return;
                }

                context.PersonMeasurings.Remove(personMeasuring);
                context.SaveChanges();
            }
        }
    }
}
