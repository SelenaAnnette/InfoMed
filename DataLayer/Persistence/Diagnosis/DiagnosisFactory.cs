namespace DataLayer.Persistence.Diagnosis
{
    using System;

    using Domain.Diagnosis;

    public class DiagnosisFactory
    {
        public Diagnosis Create(Guid id, string name, string mkb10Code)
        {
            return new Diagnosis { Id = id, Name = name, Mkb10Code = mkb10Code, Description = string.Empty };
        }

        public Diagnosis Create(Guid id, string name, string mkb10Code, string description)
        {
            return new Diagnosis { Id = id, Name = name, Mkb10Code = mkb10Code, Description = description };
        }
    }
}
