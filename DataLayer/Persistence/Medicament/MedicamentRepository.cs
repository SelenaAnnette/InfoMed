namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Medicament;

    public class MedicamentRepository : IMedicamentRepository
    {
        private readonly string ConnectionString;

        public MedicamentRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Medicament> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Medicaments.Include("AssignedMedicaments");
        }

        public Medicament GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Medicaments.Include("AssignedMedicaments").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Medicament> GetEntitiesByQuery(Func<Medicament, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Medicaments.Include("AssignedMedicaments").Where(query).ToList();
            }                                    
        }

        public Medicament CreateOrUpdateEntity(Medicament entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Medicaments.Add(entity);
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
                var medicament = context.Medicaments.FirstOrDefault(v => v.Id == id);
                if (medicament == null)
                {
                    return;
                }

                context.Medicaments.Remove(medicament);
                context.SaveChanges();
            }
        }
    }
}
