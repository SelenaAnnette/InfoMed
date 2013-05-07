namespace DataLayer.Persistence.Complaint
{
    using System;

    using Domain.Complaint;

    public class PersonConsultationComplaintFactory
    {
        public PersonConsultationComplaint Create(Guid id, Guid personConsultationId)
        {
            return new PersonConsultationComplaint { Id = id, PersonConsultationId = personConsultationId, Description = string.Empty };
        }

        public PersonConsultationComplaint Create(Guid id, Guid personConsultationId, string description)
        {
            return new PersonConsultationComplaint { Id = id, PersonConsultationId = personConsultationId, Description = description };
        }
    }
}
