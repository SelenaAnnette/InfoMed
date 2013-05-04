namespace DataLayer.Persistence.Diagnosis
{
    using System;

    using Domain.Diagnosis;

    public class PersonConsultationDiagnosisFactory
    {
        public PersonConsultationDiagnosis Create(Guid id, Guid consultationId, Guid diagnosisId, Guid diagnosisTypeId)
        {
            return new PersonConsultationDiagnosis { Id = id, ConsultationId = consultationId, DiagnosisId = diagnosisId, DiagnosisTypeId = diagnosisTypeId };
        }

        public PersonConsultationDiagnosis Create(Guid id, Guid consultationId, Guid diagnosisId, Guid diagnosisTypeId, Guid parentPersonConsultationDiagnosisId)
        {
            return new PersonConsultationDiagnosis { Id = id, ConsultationId = consultationId, DiagnosisId = diagnosisId, DiagnosisTypeId = diagnosisTypeId, ParentPersonConsultationDiagnosisId = parentPersonConsultationDiagnosisId };
        }
    }
}
