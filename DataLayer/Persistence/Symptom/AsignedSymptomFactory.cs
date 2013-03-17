namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class AsignedSymptomFactory
    {
        public AsignedSymptom Create(Guid personId, Guid symptomId)
        {
            return new AsignedSymptom { PersonId = personId, SymptomId = symptomId, IsActual = true };
        }
    }
}
