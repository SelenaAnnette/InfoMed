namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class PersonConsultationSymptomFactory
    {
        public PersonConsultationSymptom Create(Guid id, Guid personConsultationId, Guid symptomId)
        {
            return new PersonConsultationSymptom { Id = id, PersonConsultationId = personConsultationId, SymptomId = symptomId };
        }
    }
}
