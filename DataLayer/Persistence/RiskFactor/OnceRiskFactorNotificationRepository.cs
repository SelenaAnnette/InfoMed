namespace DataLayer.Persistence.RiskFactor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.RiskFactor;

    public class OnceRiskFactorNotificationRepository : IOnceRiskFactorNotificationRepository
    {
        private readonly string ConnectionString;

        public OnceRiskFactorNotificationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<OnceRiskFactorNotification> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.OnceRiskFactorNotifications;
        }

        public OnceRiskFactorNotification GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.OnceRiskFactorNotifications.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<OnceRiskFactorNotification> GetEntitiesByQuery(Func<OnceRiskFactorNotification, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.OnceRiskFactorNotifications.Where(query).ToList();
            }                                    
        }

        public OnceRiskFactorNotification CreateOrUpdateEntity(OnceRiskFactorNotification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.OnceRiskFactorNotifications.Add(entity);
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
                var onceRiskFactorNotification = context.OnceRiskFactorNotifications.FirstOrDefault(v => v.Id == id);
                if (onceRiskFactorNotification == null)
                {
                    return;
                }

                context.OnceRiskFactorNotifications.Remove(onceRiskFactorNotification);
                context.SaveChanges();
            }
        }
    }
}
