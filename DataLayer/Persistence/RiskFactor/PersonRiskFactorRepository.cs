namespace DataLayer.Persistence.RiskFactor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.RiskFactor;

    public class PersonRiskFactorRepository : IPersonRiskFactorRepository
    {
        private readonly string ConnectionString;

        public PersonRiskFactorRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonRiskFactor> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.PersonRiskFactors;
        }

        public PersonRiskFactor GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonRiskFactors.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonRiskFactor> GetEntitiesByQuery(Func<PersonRiskFactor, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.PersonRiskFactors.Where(query);
            }                                    
        }

        public PersonRiskFactor CreateOrUpdateEntity(PersonRiskFactor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonRiskFactors.Add(entity);
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
                var personRiskFactor = context.PersonRiskFactors.FirstOrDefault(v => v.Id == id);
                if (personRiskFactor == null)
                {
                    return;
                }

                context.PersonRiskFactors.Remove(personRiskFactor);
                context.SaveChanges();
            }
        }
    }
}
