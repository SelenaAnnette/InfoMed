namespace DataLayer.Persistence.Hospital
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Hospital;

    public class HospitalDepartmentRepository : IHospitalDepartmentRepository
    {
        private readonly string ConnectionString;

        public HospitalDepartmentRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<HospitalDepartment> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.HospitalDepartments.Include("Hospital").Include("PersonHospitalizations");
        }

        public HospitalDepartment GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.HospitalDepartments.Include("Hospital").Include("PersonHospitalizations").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<HospitalDepartment> GetEntitiesByQuery(Func<HospitalDepartment, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.HospitalDepartments.Include("Hospital").Include("PersonHospitalizations").Where(query).ToList();
            }                                    
        }

        public HospitalDepartment CreateOrUpdateEntity(HospitalDepartment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.HospitalDepartments.Add(entity);
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
                var hospitalDepartment = context.HospitalDepartments.FirstOrDefault(v => v.Id == id);
                if (hospitalDepartment == null)
                {
                    return;
                }

                context.HospitalDepartments.Remove(hospitalDepartment);
                context.SaveChanges();
            }
        }
    }
}
