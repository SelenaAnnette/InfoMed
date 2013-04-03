namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class PersonRiskFactorFactory
    {
        public PersonRiskFactor Create(Guid id, Guid personId, Guid riskFactorId, double value, DateTime recordingDate)
        {
            this.Validate(value);

            return new PersonRiskFactor { Id = id, PersonId = personId, RiskFactorId = riskFactorId, Value = value, RecordingDate = recordingDate };
        }

        private void Validate(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value should be a positive number");
            }
        }
    }
}
