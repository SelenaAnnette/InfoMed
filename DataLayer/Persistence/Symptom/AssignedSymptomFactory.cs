namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class AssignedSymptomFactory
    {
        public AssignedSymptom Create(Guid personId, Guid symptomId)
        {
            return new AssignedSymptom { PersonId = personId, SymptomId = symptomId, IsActual = true };
        }
    }
}
