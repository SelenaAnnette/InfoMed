namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class AssignedRiskFactorFactory
    {
        public AssignedRiskFactor Create(Guid personId, Guid riskFactorId, string measure, double value)
        {
            this.Validate(value);

            return new AssignedRiskFactor { PersonId = personId, RiskFactorId = riskFactorId, IsActual = true, Measure = measure, Value = value };
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
