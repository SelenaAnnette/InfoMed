namespace DataLayer.Persistence.LabAnalyze
{
    using System;

    using Domain.LabAnalyze;

    public class PersonConsultationLabAnalyzeFactory
    {
        public PersonConsultationLabAnalyze Create(Guid id, Guid personConsultationId, Guid labAnalyzeId, double value)
        {
            return new PersonConsultationLabAnalyze { Id = id, PersonConsultationId = personConsultationId, LabAnalyzeId = labAnalyzeId, Value = value };
        }
    }
}
