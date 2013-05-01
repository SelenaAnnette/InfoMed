namespace DataLayer.Persistence.Disease
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Disease;

    public class PersonDiseaseRepository : IPersonDiseaseRepository
    {
        private readonly string ConnectionString;

        public PersonDiseaseRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonDisease> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonDiseases.Include("Person").Include("Disease");
        }

        public PersonDisease GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonDiseases.Include("Person").Include("Disease").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonDisease> GetEntitiesByQuery(Func<PersonDisease, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonDiseases.Include("Person").Include("Disease").Where(query).ToList();
            }                                    
        }

        public PersonDisease CreateOrUpdateEntity(PersonDisease entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonDiseases.Add(entity);
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
                var personDisease = context.PersonDiseases.FirstOrDefault(v => v.Id == id);
                if (personDisease == null)
                {
                    return;
                }

                context.PersonDiseases.Remove(personDisease);
                context.SaveChanges();
            }
        }
    }
}
