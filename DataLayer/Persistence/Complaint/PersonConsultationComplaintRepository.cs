namespace DataLayer.Persistence.Complaint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data;

    using Domain.Complaint;

    public class PersonConsultationComplaintRepository : IPersonConsultationComplaintRepository
    {
        private readonly string ConnectionString;

        public PersonConsultationComplaintRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IEnumerable<PersonConsultationComplaint> GetAll()
        {
            var context = new DomainContext(this.ConnectionString);
            return context.PersonConsultationComplaints.Include("PersonConsultation");
        }

        public PersonConsultationComplaint GetEntityById(Guid id)
        {
            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationComplaints.Include("PersonConsultation")
                    .FirstOrDefault(v => v.Id == id);
            }
        }

        public IEnumerable<PersonConsultationComplaint> GetEntitiesByQuery(Func<PersonConsultationComplaint, bool> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {
                return context.PersonConsultationComplaints.Include("PersonConsultation")
                    .Where(query).ToList();
            }                                    
        }

        public PersonConsultationComplaint CreateOrUpdateEntity(PersonConsultationComplaint entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            using (var context = new DomainContext(this.ConnectionString))
            {                                
                if (this.GetEntityById(entity.Id) == null)
                {
                    context.PersonConsultationComplaints.Add(entity);
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
                var personConsultationComplaint = context.PersonConsultationComplaints.FirstOrDefault(v => v.Id == id);
                if (personConsultationComplaint == null)
                {
                    return;
                }

                context.PersonConsultationComplaints.Remove(personConsultationComplaint);
                context.SaveChanges();
            }
        }
    }
}
