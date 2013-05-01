namespace DataLayer.Persistence.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Operation;

    public class OperationRepository : IOperationRepository
    {
        private readonly string ConnectionString;

        public OperationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Operation> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Operations.Include("PersonOperations");
        }

        public Operation GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Operations.Include("PersonOperations").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Operation> GetEntitiesByQuery(Func<Operation, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Operations.Include("PersonOperations").Where(query).ToList();
            }                                    
        }

        public Operation CreateOrUpdateEntity(Operation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Operations.Add(entity);
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
                var operation = context.Operations.FirstOrDefault(v => v.Id == id);
                if (operation == null)
                {
                    return;
                }

                context.Operations.Remove(operation);
                context.SaveChanges();
            }
        }
    }
}
