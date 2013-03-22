namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class AssignedRiskFactorFactory
    {
        public AssignedRiskFactor Create(Guid personId, Guid riskFactorId, string measure, double value)
        {
            return new AssignedRiskFactor { PersonId = personId, RiskFactorId = riskFactorId, IsActual = true, Measure = measure, Value = value };
        }
    }
}
