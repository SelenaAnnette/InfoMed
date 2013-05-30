namespace DataLayer.Persistence.Sms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;    

    using Domain.Sms;

    public class DomainSmsRepository : IDomainSmsRepository
    {
        private readonly string ConnectionString;

        public DomainSmsRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<DomainSms> GetAll()
        {
            var context = new TrashDomainContext(this.ConnectionString);
            return context.DomainSmses;
        }

        public DomainSms GetEntityById(Guid id)
        {
            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.DomainSmses.FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<DomainSms> GetEntitiesByQuery(Func<DomainSms, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {
                return context.DomainSmses.Where(query).ToList();
            }                                    
        }

        public DomainSms CreateOrUpdateEntity(DomainSms entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new TrashDomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.DomainSmses.Add(entity);
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
                var domainSms = context.DomainSmses.FirstOrDefault(v => v.Id == id);
                if (domainSms == null)
                {
                    return;
                }

                context.DomainSmses.Remove(domainSms);
                context.SaveChanges();
            }
        }
    }
}
