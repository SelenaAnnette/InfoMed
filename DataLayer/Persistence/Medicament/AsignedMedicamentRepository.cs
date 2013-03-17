namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Medicament;

    public class AsignedMedicamentRepository : IAsignedMedicamentRepository
    {
        private readonly string ConnectionString;

        public AsignedMedicamentRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AsignedMedicament> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AsignedMedicaments.Include("Person").Include("Medicament");
        }

        public AsignedMedicament GetEntityById(Guid id)
        {
            throw new NotImplementedException("this method is not implemented");
        }

        public IEnumerable<AsignedMedicament> GetEntitiesByQuery(Func<AsignedMedicament, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AsignedMedicaments.Include("Person").Include("Medicament").Where(query);
            }                                    
        }

        public AsignedMedicament CreateOrUpdateEntity(AsignedMedicament entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntitiesByQuery(v => v.PersonId == entity.PersonId && v.MedicamentId == entity.MedicamentId) == null)
                {
                    context.AsignedMedicaments.Add(entity);
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
            throw new NotImplementedException("this method is not implemented");
        }
    }
}
