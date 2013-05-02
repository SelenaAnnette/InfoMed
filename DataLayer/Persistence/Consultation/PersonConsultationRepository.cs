namespace DataLayer.Persistence.Consultation
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Domain.Consultation;

    public class PersonConsultationRepository : IPersonConsultationRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultation> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultations.Include("Doctor").Include("Patient").Include("ConsultationType").Include("PersonConsultationResearches")
                .Include("PersonConsultationLabAnalyzes").Include("PersonConsultationSymptoms").Include("PersonConsultationComplaints")
                .Include("PersonConsultationMeasurings").Include("PersonConsultationDiagnosises").Include("AssignedMedicaments");
        }

        public PersonConsultation GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultations.Include("Doctor").Include("Patient").Include("ConsultationType").Include("PersonConsultationResearches")
                    .Include("PersonConsultationLabAnalyzes").Include("PersonConsultationSymptoms").Include("PersonConsultationComplaints")
                    .Include("PersonConsultationMeasurings").Include("PersonConsultationDiagnosises").Include("AssignedMedicaments")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultation> GetEntitiesByQuery(Func<PersonConsultation, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultations.Include("Doctor").Include("Patient").Include("ConsultationType").Include("PersonConsultationResearches")
                    .Include("PersonConsultationLabAnalyzes").Include("PersonConsultationSymptoms").Include("PersonConsultationComplaints")
                    .Include("PersonConsultationMeasurings").Include("PersonConsultationDiagnosises").Include("AssignedMedicaments")
                    .Where(query).ToList();
            }                                    
        }

        public PersonConsultation CreateOrUpdateEntity(PersonConsultation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultations.Add(entity);
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
                var personConsultation = context.PersonConsultations.FirstOrDefault(v => v.Id == id);
                if (personConsultation == null)
                {
                    return;
                }

                context.PersonConsultations.Remove(personConsultation);
                context.SaveChanges();
            }
        }
    }
}
