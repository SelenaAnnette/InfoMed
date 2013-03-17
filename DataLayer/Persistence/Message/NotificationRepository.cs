namespace DataLayer.Persistence.Message
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Message;

    public class NotificationRepository : INotificationRepository
    {
        private readonly string ConnectionString;

        public NotificationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Notification> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.Notifications;
        }

        public Notification GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.Notifications.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Notification> GetEntitiesByQuery(Func<Notification, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.Notifications.Where(query);
            }                                    
        }

        public Notification CreateOrUpdateEntity(Notification entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Notifications.Add(entity);
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
                var notification = context.Notifications.FirstOrDefault(v => v.Id == id);
                if (notification == null)
                {
                    return;
                }

                context.Notifications.Remove(notification);
                context.SaveChanges();
            }
        }
    }
}
