namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class PersonConsultationSymptomFactory
    {
        public PersonConsultationSymptom Create(Guid id, Guid consultationId, Guid symptomId)
        {
            return new PersonConsultationSymptom { Id = id, ConsultationId = consultationId, SymptomId = symptomId };
        }
    }
}
