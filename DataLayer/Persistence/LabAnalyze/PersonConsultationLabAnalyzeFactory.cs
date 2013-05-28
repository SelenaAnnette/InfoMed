namespace DataLayer.Persistence.LabAnalyze
{
    using System;

    using Domain.LabAnalyze;

    public class PersonConsultationLabAnalyzeFactory
    {
        public PersonConsultationLabAnalyze Create(Guid id, Guid personConsultationId, Guid labAnalyzeTypeId, double value)
        {
            return new PersonConsultationLabAnalyze { Id = id, PersonConsultationId = personConsultationId, LabAnalyzeTypeId = labAnalyzeTypeId, Value = value };
        }
    }
}
