namespace DataLayer.Persistence.Complaint
{
    using System;

    using Domain.Complaint;

    public class PersonConsultationComplaintFactory
    {
        public PersonConsultationComplaint Create(Guid id, Guid consultationId)
        {
            return new PersonConsultationComplaint { Id = id, ConsultationId = consultationId, Description = string.Empty };
        }

        public PersonConsultationComplaint Create(Guid id, Guid consultationId, string description)
        {
            return new PersonConsultationComplaint { Id = id, ConsultationId = consultationId, Description = description };
        }
    }
}
