namespace DataLayer.Persistence.Consultation
{
    using System;

    using Domain.Consultation;

    public class PersonConsultationFactory
    {
        public PersonConsultation Create(Guid id, Guid patientId, Guid doctorId, Guid consultationTypeId, DateTime consultationDate)
        {
            return new PersonConsultation { Id = id, PatientId = patientId, DoctorId = doctorId, ConsultationTypeId = consultationTypeId, ConsultationDate = consultationDate };
        }
    }
}
