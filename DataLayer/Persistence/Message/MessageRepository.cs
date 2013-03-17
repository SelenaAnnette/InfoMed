namespace DataLayer.Persistence.Message
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Message;

    public class MessageRepository : IMessageRepository
    {
        private readonly string ConnectionString;

        public MessageRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Message> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.Messages;
        }

        public Message GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.Messages.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Message> GetEntitiesByQuery(Func<Message, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.Messages.Where(query);
            }                                    
        }

        public Message CreateOrUpdateEntity(Message entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Messages.Add(entity);
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
                var message = context.Messages.FirstOrDefault(v => v.Id == id);
                if (message == null)
                {
                    return;
                }

                context.Messages.Remove(message);
                context.SaveChanges();
            }
        }
    }
}
