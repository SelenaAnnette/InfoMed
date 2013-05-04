namespace DataLayer.Persistence.Diagnosis
{
    using System;

    using Domain.Diagnosis;

    public class DiagnosisTypeFactory
    {
        public DiagnosisType Create(Guid id, string name)
        {
            return new DiagnosisType { Id = id, Name = name, Description = string.Empty };
        }

        public DiagnosisType Create(Guid id, string name, string description)
        {
            return new DiagnosisType { Id = id, Name = name, Description = description };
        }
    }
}
