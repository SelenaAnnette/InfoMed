namespace DataLayer.Persistence.LabAnalyze
{
    using System;

    using Domain.LabAnalyze;

    public class PersonConsultationLabAnalyzeFactory
    {
        public PersonConsultationLabAnalyze Create(Guid id, Guid consultationId, Guid labAnalyzeId, double value)
        {
            return new PersonConsultationLabAnalyze { Id = id, ConsultationId = consultationId, LabAnalyzeId = labAnalyzeId, Value = value };
        }
    }
}
