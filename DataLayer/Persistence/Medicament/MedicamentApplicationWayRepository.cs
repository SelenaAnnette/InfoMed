namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Medicament;

    public class MedicamentApplicationWayRepository : IMedicamentApplicationWayRepository
    {
        private readonly string ConnectionString;

        public MedicamentApplicationWayRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<MedicamentApplicationWay> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.MedicamentApplicationWays.Include("AssignedMedicaments");
        }

        public MedicamentApplicationWay GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MedicamentApplicationWays.Include("AssignedMedicaments")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<MedicamentApplicationWay> GetEntitiesByQuery(Func<MedicamentApplicationWay, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MedicamentApplicationWays.Include("AssignedMedicaments")
                    .Where(query).ToList();
            }                                    
        }

        public MedicamentApplicationWay CreateOrUpdateEntity(MedicamentApplicationWay entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.MedicamentApplicationWays.Add(entity);
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
                var medicamentApplicationWay = context.MedicamentApplicationWays.FirstOrDefault(v => v.Id == id);
                if (medicamentApplicationWay == null)
                {
                    return;
                }

                context.MedicamentApplicationWays.Remove(medicamentApplicationWay);
                context.SaveChanges();
            }
        }
    }
}
