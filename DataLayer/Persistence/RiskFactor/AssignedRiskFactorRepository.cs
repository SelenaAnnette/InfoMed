namespace DataLayer.Persistence.RiskFactor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.RiskFactor;

    public class AssignedRiskFactorRepository : IAssignedRiskFactorRepository
    {
        private readonly string ConnectionString;

        public AssignedRiskFactorRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<AssignedRiskFactor> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.AssignedRiskFactors.Include("Person").Include("RiskFactor");
        }

        public AssignedRiskFactor GetEntityById(Guid id)
        {
            throw new NotImplementedException("this method is not implemented");
        }

        public IEnumerable<AssignedRiskFactor> GetEntitiesByQuery(Func<AssignedRiskFactor, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.AssignedRiskFactors.Include("Person").Include("RiskFactor").Where(query);
            }                                    
        }

        public AssignedRiskFactor CreateOrUpdateEntity(AssignedRiskFactor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntitiesByQuery(v => v.PersonId == entity.PersonId && v.RiskFactorId == entity.RiskFactorId) == null)
                {
                    context.AssignedRiskFactors.Add(entity);
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
