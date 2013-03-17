namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class PersonRiskFactorFactory
    {
        public PersonRiskFactor Create(Guid id, Guid personId, Guid riskFactorId, double value, DateTime recordingDate)
        {
            return new PersonRiskFactor { Id = id, PersonId = personId, RiskFactorId = riskFactorId, Value = value, RecordingDate = recordingDate };
        }
    }
}
