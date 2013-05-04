namespace DataLayer.Persistence.Hospital
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Hospital;

    public class HospitalRepository : IHospitalRepository
    {
        private readonly string ConnectionString;

        public HospitalRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<Hospital> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.Hospitals.Include("HospitalDepartments");
        }

        public Hospital GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Hospitals.Include("HospitalDepartments").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<Hospital> GetEntitiesByQuery(Func<Hospital, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.Hospitals.Include("HospitalDepartments").Where(query).ToList();
            }                                    
        }

        public Hospital CreateOrUpdateEntity(Hospital entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.Hospitals.Add(entity);
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
                var hospital = context.Hospitals.FirstOrDefault(v => v.Id == id);
                if (hospital == null)
                {
                    return;
                }

                context.Hospitals.Remove(hospital);
                context.SaveChanges();
            }
        }
    }
}
