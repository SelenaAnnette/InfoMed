namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Medicament;

    public class MedicamentFormRepository : IMedicamentFormRepository
    {
        private readonly string ConnectionString;

        public MedicamentFormRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<MedicamentForm> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.MedicamentForms.Include("Medicaments");
        }

        public MedicamentForm GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MedicamentForms.Include("Medicaments")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<MedicamentForm> GetEntitiesByQuery(Func<MedicamentForm, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.MedicamentForms.Include("Medicaments")
                    .Where(query).ToList();
            }                                    
        }

        public MedicamentForm CreateOrUpdateEntity(MedicamentForm entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.MedicamentForms.Add(entity);
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
                var medicamentForm = context.MedicamentForms.FirstOrDefault(v => v.Id == id);
                if (medicamentForm == null)
                {
                    return;
                }

                context.MedicamentForms.Remove(medicamentForm);
                context.SaveChanges();
            }
        }
    }
}
