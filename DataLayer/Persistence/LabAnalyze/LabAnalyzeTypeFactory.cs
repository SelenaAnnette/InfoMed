namespace DataLayer.Persistence.LabAnalyze
{
    using System;

    using Domain.LabAnalyze;

    public class LabAnalyzeTypeFactory
    {
        public LabAnalyzeType Create(Guid id, string measure)
        {
            return new LabAnalyzeType { Id = id, Measure = measure, Description = string.Empty };
        }

        public LabAnalyzeType Create(Guid id, string measure, string description)
        {
            return new LabAnalyzeType { Id = id, Measure = measure, Description = description };
        }
    }
}
