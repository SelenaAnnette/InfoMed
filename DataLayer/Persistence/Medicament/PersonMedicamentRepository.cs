namespace DataLayer.Persistence.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;
   
    using Domain.Medicament;

    public class PersonMedicamentRepository : IPersonMedicamentRepository
    {
        private readonly string ConnectionString;

        public PersonMedicamentRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonMedicament> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.PersonMedicaments;
        }

        public PersonMedicament GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonMedicaments.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonMedicament> GetEntitiesByQuery(Func<PersonMedicament, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonMedicaments.Where(query).ToList();
            }                                    
        }

        public PersonMedicament CreateOrUpdateEntity(PersonMedicament entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonMedicaments.Add(entity);
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
                var personMedicament = context.PersonMedicaments.FirstOrDefault(v => v.Id == id);
                if (personMedicament == null)
                {
                    return;
                }

                context.PersonMedicaments.Remove(personMedicament);
                context.SaveChanges();
            }
        }
    }
}
