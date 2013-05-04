namespace DataLayer.Persistence.Hospital
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Hospital;

    public class PersonHospitalizationRepository : IPersonHospitalizationRepository
    {
        private readonly string ConnectionString;

        public PersonHospitalizationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonHospitalization> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonHospitalizations.Include("HospitalDepartment").Include("Person");
        }

        public PersonHospitalization GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonHospitalizations.Include("HospitalDepartment").Include("Person").FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonHospitalization> GetEntitiesByQuery(Func<PersonHospitalization, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonHospitalizations.Include("HospitalDepartment").Include("Person").Where(query).ToList();
            }                                    
        }

        public PersonHospitalization CreateOrUpdateEntity(PersonHospitalization entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonHospitalizations.Add(entity);
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
                var personHospitalization = context.PersonHospitalizations.FirstOrDefault(v => v.Id == id);
                if (personHospitalization == null)
                {
                    return;
                }

                context.PersonHospitalizations.Remove(personHospitalization);
                context.SaveChanges();
            }
        }
    }
}
