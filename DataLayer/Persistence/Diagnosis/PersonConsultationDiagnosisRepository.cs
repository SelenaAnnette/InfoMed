namespace DataLayer.Persistence.Diagnosis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Diagnosis;

    public class PersonConsultationDiagnosisRepository : IPersonConsultationDiagnosisRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationDiagnosisRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationDiagnosis> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationDiagnosises.Include("PersonConsultationDiagnosises").Include("ParentPersonConsultationDiagnosis").Include("DiagnosisType")
                .Include("Diagnosis").Include("PersonConsultation");
        }

        public PersonConsultationDiagnosis GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationDiagnosises.Include("PersonConsultationDiagnosises").Include("ParentPersonConsultationDiagnosis").Include("DiagnosisType")
                .Include("Diagnosis").Include("PersonConsultation")
                .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationDiagnosis> GetEntitiesByQuery(Func<PersonConsultationDiagnosis, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationDiagnosises.Include("PersonConsultationDiagnosises").Include("ParentPersonConsultationDiagnosis").Include("DiagnosisType")
                .Include("Diagnosis").Include("PersonConsultation")
                .Where(query).ToList();
            }                                    
        }

        public PersonConsultationDiagnosis CreateOrUpdateEntity(PersonConsultationDiagnosis entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationDiagnosises.Add(entity);
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
                var personConsultationDiagnosis = context.PersonConsultationDiagnosises.FirstOrDefault(v => v.Id == id);
                if (personConsultationDiagnosis == null)
                {
                    return;
                }

                context.PersonConsultationDiagnosises.Remove(personConsultationDiagnosis);
                context.SaveChanges();
            }
        }
    }
}
