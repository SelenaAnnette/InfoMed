namespace DataLayer.Persistence.RiskFactor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.RiskFactor;

    public class AsignedRiskFactorRepository : IAsignedRiskFactorRepository
    {
        private readonly string ConnectionString;

        public AsignedRiskFactorRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AsignedRiskFactor> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AsignedRiskFactors.Include("Person").Include("RiskFactor");
        }

        public AsignedRiskFactor GetEntityById(Guid id)
        {
            throw new NotImplementedException("this method is not implemented");
        }

        public IEnumerable<AsignedRiskFactor> GetEntitiesByQuery(Func<AsignedRiskFactor, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AsignedRiskFactors.Include("Person").Include("RiskFactor").Where(query);
            }                                    
        }

        public AsignedRiskFactor CreateOrUpdateEntity(AsignedRiskFactor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntitiesByQuery(v => v.PersonId == entity.PersonId && v.RiskFactorId == entity.RiskFactorId) == null)
                {
                    context.AsignedRiskFactors.Add(entity);
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
