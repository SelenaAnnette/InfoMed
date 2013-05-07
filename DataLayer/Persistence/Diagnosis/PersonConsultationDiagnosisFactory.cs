namespace DataLayer.Persistence.Diagnosis
{
    using System;

    using Domain.Diagnosis;

    public class PersonConsultationDiagnosisFactory
    {
        public PersonConsultationDiagnosis Create(Guid id, Guid personConsultationId, Guid diagnosisId, Guid diagnosisTypeId)
        {
            return new PersonConsultationDiagnosis { Id = id, PersonConsultationId = personConsultationId, DiagnosisId = diagnosisId, DiagnosisTypeId = diagnosisTypeId };
        }

        public PersonConsultationDiagnosis Create(Guid id, Guid personConsultationId, Guid diagnosisId, Guid diagnosisTypeId, Guid parentPersonConsultationDiagnosisId)
        {
            return new PersonConsultationDiagnosis { Id = id, PersonConsultationId = personConsultationId, DiagnosisId = diagnosisId, DiagnosisTypeId = diagnosisTypeId, ParentPersonConsultationDiagnosisId = parentPersonConsultationDiagnosisId };
        }
    }
}
