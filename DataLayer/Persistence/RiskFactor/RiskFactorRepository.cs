namespace DataLayer.Persistence.RiskFactor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.RiskFactor;

    public class RiskFactorRepository : IRiskFactorRepository
    {
        private readonly string ConnectionString;

        public RiskFactorRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<RiskFactor> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.RiskFactors.Include("AsignedRiskFactors");
        }

        public RiskFactor GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.RiskFactors.Include("AsignedRiskFactors").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<RiskFactor> GetEntitiesByQuery(Func<RiskFactor, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.RiskFactors.Include("AsignedRiskFactors").Where(query);
            }                                    
        }

        public RiskFactor CreateOrUpdateEntity(RiskFactor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.RiskFactors.Add(entity);
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
                var riskFactor = context.RiskFactors.FirstOrDefault(v => v.Id == id);
                if (riskFactor == null)
                {
                    return;
                }

                context.RiskFactors.Remove(riskFactor);
                context.SaveChanges();
            }
        }
    }
}
