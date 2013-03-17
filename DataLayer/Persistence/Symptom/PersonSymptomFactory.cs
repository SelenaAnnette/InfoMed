namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class PersonSymptomFactory
    {
        public PersonSymptom Create(Guid id, Guid personId, Guid symptomId, DateTime recordingDate)
        {
            return new PersonSymptom { Id = id, PersonId = personId, SymptomId = symptomId, RecordingDate = recordingDate };
        }
    }
}
