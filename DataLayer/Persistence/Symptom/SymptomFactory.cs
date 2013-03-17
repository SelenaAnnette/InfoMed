namespace DataLayer.Persistence.Symptom
{
    using System;

    using Domain.Symptom;

    public class SymptomFactory
    {
        public Symptom Create(Guid id, string name)
        {
            return new Symptom { Id = id, Name = name, Description = string.Empty };
        }
    }
}
