namespace DataLayer.Persistence.Message
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Data;    

    using Domain.Message;

    public class MeasuringNotificationRepository : IMeasuringNotificationRepository
    {
        private readonly string ConnectionString;

        public MeasuringNotificationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<MeasuringNotification> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.MeasuringNotifications;
        }

        public MeasuringNotification GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.MeasuringNotifications.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<MeasuringNotification> GetEntitiesByQuery(Func<MeasuringNotification, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.MeasuringNotifications.Where(query).ToList();
            }                                    
        }

        public MeasuringNotification CreateOrUpdateEntity(MeasuringNotification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.MeasuringNotifications.Add(entity);
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
                var measuringNotification = context.MeasuringNotifications.FirstOrDefault(v => v.Id == id);
                if (measuringNotification == null)
                {
                    return;
                }

                context.MeasuringNotifications.Remove(measuringNotification);
                context.SaveChanges();
            }
        }
    }
}
