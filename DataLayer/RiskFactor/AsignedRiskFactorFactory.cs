namespace DataLayer.Persistence.RiskFactor
{
    using System;

    using Domain.RiskFactor;

    public class AsignedRiskFactorFactory
    {
        public AsignedRiskFactor Create(Guid personId, Guid riskFactorId, string measure, double value)
        {
            return new AsignedRiskFactor { PersonId = personId, RiskFactorId = riskFactorId, IsActual = true, Measure = measure, Value = value };
        }
    }
}
